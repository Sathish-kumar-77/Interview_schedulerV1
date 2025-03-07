using System;
using Assignment.Contracts.Data;
using Assignment.Contracts.Data.Entities;
using Assignment.Contracts.DTO;
using Assignment.Core.Exceptions;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Assignment.Core.Handlers.Commands;


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

            var result = _validator.Validate(model);

            if (!result.IsValid)
            {
                var errors = result.Errors.Select(x => x.ErrorMessage).ToArray();
                throw new InvalidRequestBodyException
                {
                    Errors = errors
                };
            }


            var entity = new Users
            {
                UserId = model.UserId,
                Name = model.Name,
                Email = model.Email,
                ReportingManager = model.ReportingManager,
                Designation = model.Designation,
                Password = model.Password,
                RoleId = model.RoleId,

            };
             entity.Password= _passwordHasher.HashPassword(entity, model.Password);
            _repository.Users.Add(entity);
            await _repository.CommitAsync();

            return entity.UserId;
        }
    }
