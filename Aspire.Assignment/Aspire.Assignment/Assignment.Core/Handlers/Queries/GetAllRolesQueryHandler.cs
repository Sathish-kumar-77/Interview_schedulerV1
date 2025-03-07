using Assignment.Contracts.Data;
using Assignment.Contracts.DTO;

using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Assignment.Core.Handlers.QueryHandlers
{   
    public class GetAllRolesQuery : IRequest<IEnumerable<RoleDTO>> { 

    }
     
    public class GetAllRolesQueryHandler : IRequestHandler<GetAllRolesQuery, IEnumerable<RoleDTO>>
    {
        private readonly IUnitOfWork _repository;
    
        public GetAllRolesQueryHandler(IUnitOfWork repository)
        {
            _repository = repository;
        }
    
        public async Task<IEnumerable<RoleDTO>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
        {
            var roles =  _repository.Roles.GetAll();
            return roles.Select(role => new RoleDTO { RoleId = role.RoleId, RoleName = role.RoleName });
        }
    


   
    
}
}
