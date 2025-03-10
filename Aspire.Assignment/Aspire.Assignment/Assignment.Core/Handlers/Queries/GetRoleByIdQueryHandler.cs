using Assignment.Contracts.Data;
using Assignment.Contracts.DTO;
using Assignment.Core.Handlers.QueryHandlers;
using MediatR;


public class GetRoleByIdQuery : IRequest<RoleDTO>
    {
        public int RoleId { get; }
        public GetRoleByIdQuery(int roleId) => RoleId = roleId;
    }
public class GetRoleByIdQueryHandler : IRequestHandler<GetRoleByIdQuery, RoleDTO>
    {
        private readonly IUnitOfWork _repository;

        public GetRoleByIdQueryHandler(IUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<RoleDTO?> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {
            var role = _repository.Roles.Get(request.RoleId);
            return role != null ? new RoleDTO { RoleId = role.RoleId, RoleName = role.RoleName } : null;
        }
    }
