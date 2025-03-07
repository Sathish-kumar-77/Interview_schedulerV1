using System;
using Assignment.Contracts.Data;
using Assignment.Contracts.DTO;
using MediatR;

namespace Assignment.Core.Handlers.Queries;

public class GetAllAllocateDateQuery : IRequest<IEnumerable<AllocateDateDTO>> { }
 
    public class GetAllAllocateDateQueryHandler : IRequestHandler<GetAllAllocateDateQuery, IEnumerable<AllocateDateDTO>>
    {
        private readonly IUnitOfWork _repository;
 
        public GetAllAllocateDateQueryHandler(IUnitOfWork repository)
        {
            _repository = repository;
        }
 
        
        public async Task<IEnumerable<AllocateDateDTO>> Handle( GetAllAllocateDateQuery request, CancellationToken cancellationToken )
        {
            
            var dates = await _repository.AllocateDate.GetAllAsync();
            
            var allocateDateDTOs = new List<AllocateDateDTO>();
 
            foreach (var date in dates)
            {
                allocateDateDTOs.Add(
                    new AllocateDateDTO
                    {
                       PanelMemberID= date.PanelMemberID,
                        StartDate = date.StartDate,
                         EndDate = date.EndDate
                    }
                );
            }
 
            return allocateDateDTOs;
        }
    }
