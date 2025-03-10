using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Assignment.Contracts.Data;
using Assignment.Contracts.Data.Entities;
using Assignment.Contracts.DTO;
using Assignment.Core.Exceptions;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Assignment.Core.Handlers.Commands
{
    public class CreateUsersCommand : IRequest<int>
    {
        public CreateUsersDTO Model { get; }
        public CreateUsersCommand(CreateUsersDTO model)
        {
            this.Model = model;
        }
    }

    public class CreateUsersCommandHandler : IRequestHandler<CreateUsersCommand, int>
    {
        private readonly IUnitOfWork _repository;
        private readonly IValidator<CreateUsersDTO> _validator;
        private readonly IPasswordHasher<Users> _passwordHasher;

        public CreateUsersCommandHandler(IUnitOfWork repository, IValidator<CreateUsersDTO> validator, IPasswordHasher<Users> passwordHasher)
        {
            _repository = repository;
            _validator = validator;
            _passwordHasher = passwordHasher;
        }

      public async Task<int> Handle(CreateUsersCommand request, CancellationToken cancellationToken)
{
    CreateUsersDTO model = request.Model;

    // Validate input data
    var validationResult = _validator.Validate(model);
    if (!validationResult.IsValid)
    {
        var errors = validationResult.Errors.Select(x => x.ErrorMessage).ToArray();
        throw new InvalidRequestBodyException { Errors = errors };
    }

    // Check if Reporting Manager is provided
    if (!string.IsNullOrEmpty(model.ReportingManager))
    {
        // Get the reporting manager user record by Name
        var manager = _repository.Users.Find(u => u.Name == model.ReportingManager).FirstOrDefault();
        
        if (manager == null)
        {
            throw new InvalidRequestBodyException
            {
                Errors = new[] { "The assigned Reporting Manager does not exist." }
            };
        }

        // Get the role of the reporting manager
        var managerRole = _repository.Roles.Find(r => r.RoleId == manager.RoleId).FirstOrDefault();
        if (managerRole == null || !managerRole.RoleName.Contains("ReportingManager", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidRequestBodyException
            {
                Errors = new[] { "The assigned Reporting Manager does not have a Manager role." }
            };
        }
    }

    // ✅ Check if RoleId exists in Roles table
    var roleExists = _repository.Roles.Find(r => r.RoleId == model.RoleId).Any();
    if (!roleExists)
    {
        throw new InvalidRequestBodyException
        {
            Errors = new[] { "Invalid RoleId. The specified Role does not exist." }
        };
    }

    // Create a new user entity
    var entity = new Users
    {
        UserId = model.UserId,
        Name = model.Name,
        Email = model.Email,
        ReportingManager = model.ReportingManager,
        Designation = model.Designation,
        Password = _passwordHasher.HashPassword(null, model.Password), // Hash password
        RoleId = model.RoleId
    };

    // Save the new user
    _repository.Users.Add(entity);
    await _repository.CommitAsync();

    return entity.UserId;
}

    }
}
