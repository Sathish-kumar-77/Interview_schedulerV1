using System;
using Assignment.Contracts.Data;
using Assignment.Contracts.DTO;
using Assignment.Core.Exceptions;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Assignment.Core.Handlers.Queries;


public class GetPanelAllocationByIdQuery : IRequest<IEnumerable<AllocateDateDTO>>
{
     public int UserId {get;}
    public GetPanelAllocationByIdQuery(int userId)
    {
        UserId = userId;
    }
 }

public class GetPanelAllocationByIdQueryHandler : IRequestHandler<GetPanelAllocationByIdQuery, IEnumerable<AllocateDateDTO>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public GetPanelAllocationByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<IEnumerable<AllocateDateDTO>> Handle(GetPanelAllocationByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await Task.FromResult(_unitOfWork.Users.GetAll().FirstOrDefault(u => u.UserId == request.UserId));
        if (user == null)
            {
                throw new EntityNotFoundException($"No USER found for {request.UserId}");
            }

        var Allocations = await _unitOfWork.AllocateDate.GetPanelAllocationByUserId(user.UserId);
        if (Allocations == null || !Allocations.Any())
            {
                throw new EntityNotFoundException($"No Allocation found for {user.Name}");
            }

            return _mapper.Map<IEnumerable<AllocateDateDTO>>(Allocations);
    }
}
