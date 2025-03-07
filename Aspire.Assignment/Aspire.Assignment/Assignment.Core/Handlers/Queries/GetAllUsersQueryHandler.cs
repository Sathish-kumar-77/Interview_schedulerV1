using AutoMapper;
using MediatR;
using Assignment.Contracts.Data;
using Assignment.Contracts.DTO;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Assignment.Providers.Handlers.Queries
{

    public class GetAllUsersQuery : IRequest<IEnumerable<UsersDTO>>
    {
    }
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<UsersDTO>> 
    {
        private readonly IUnitOfWork _repository;
        private readonly IMapper _mapper;

        public GetAllUsersQueryHandler(IUnitOfWork repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UsersDTO>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await Task.Run(() => _repository.Users.GetAll(), cancellationToken); 
            return _mapper.Map<IEnumerable<UsersDTO>>(users);
        }
    }
    }
