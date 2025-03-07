
using Assignment.Contracts.Data;
using Assignment.Contracts.Data.Entities;
using Assignment.Contracts.DTO;
using Assignment.Core.Exceptions;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;

public class UpdateUserCommand : IRequest<bool>
{
    public int UserId { get; }
    public UpdateUsersDTO Model { get; }

    public UpdateUserCommand(int userId, UpdateUsersDTO model)
    {
        UserId = userId;
        Model = model;
    }
}

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, bool>
{
    private readonly IUnitOfWork _repository;
    private readonly IValidator<UpdateUsersDTO> _validator;
    private readonly IPasswordHasher<Users> _passwordHasher;

    public UpdateUserCommandHandler(IUnitOfWork repository, IValidator<UpdateUsersDTO> validator, IPasswordHasher<Users> passwordHasher)
    {
        _repository = repository;
        _validator = validator;
        _passwordHasher = passwordHasher;
    }

    public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var model = request.Model;
        var user = _repository.Users.Get(request.UserId);

        if (user == null)
        {
            throw new KeyNotFoundException($"User with ID {request.UserId} not found.");
        }

        // Validate only the provided fields
        var validationResult = _validator.Validate(model);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToArray();
            throw new InvalidRequestBodyException { Errors = errors };
        }

        // Update only the provided fields
        if (!string.IsNullOrEmpty(model.Name)) user.Name = model.Name;
        if (!string.IsNullOrEmpty(model.Email)) user.Email = model.Email;
        if (!string.IsNullOrEmpty(model.ReportingManager)) user.ReportingManager = model.ReportingManager;
        if (!string.IsNullOrEmpty(model.Designation)) user.Designation = model.Designation;
        if (model.RoleId.HasValue) user.RoleId = model.RoleId.Value;

        // If a new password is provided, hash and update it
        if (!string.IsNullOrEmpty(model.Password))
        {
            user.Password = _passwordHasher.HashPassword(user, model.Password);
        }

        _repository.Users.Update(user);
        await _repository.CommitAsync();

        return true;
    }
}
