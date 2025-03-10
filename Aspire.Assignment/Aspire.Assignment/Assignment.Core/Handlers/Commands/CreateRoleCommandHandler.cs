
using Assignment.Contracts.Data;
using Assignment.Contracts.Data.Entities;
using Assignment.Contracts.DTO;

using Assignment.Core.Exceptions;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

public class CreateRoleCommand : IRequest<int>
    {
        public CreateRoleDTO Model { get; }
        public CreateRoleCommand(CreateRoleDTO model) => Model = model;
    }
public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, int>
    {
        private readonly IUnitOfWork _repository;
        private readonly IValidator<CreateRoleDTO> _validator;

        public CreateRoleCommandHandler(IUnitOfWork repository, IValidator<CreateRoleDTO> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<int> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            var model = request.Model;

            // Validate input
            var result = _validator.Validate(model);
            if (!result.IsValid)
            {
                var errors = result.Errors.Select(x => x.ErrorMessage).ToArray();
                throw new InvalidRequestBodyException { Errors = errors };
            }

            // Create Role entity
            var entity = new Role
            {
                RoleName = model.RoleName
            };

            _repository.Roles.Add(entity);
            await _repository.CommitAsync();

            return entity.RoleId;
        }
    }

