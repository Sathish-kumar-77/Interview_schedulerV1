using System;
using Assignment.Contracts.Data;
using Assignment.Contracts.Data.Entities;
using Assignment.Contracts.DTO;
using Assignment.Core.Exceptions;
using AutoMapper;
using MediatR;

namespace Assignment.Core.Handlers.Queries;

public class GetSlotByIdQuery : IRequest<IEnumerable<SlotDetailsDTO>>
{
    public string Name {get;}
    public GetSlotByIdQuery(string name)
    {
        Name = name;
        
    }
}

public class GetSlotByIdQueryHandler : IRequestHandler<GetSlotByIdQuery, IEnumerable<SlotDetailsDTO>>
{

    private readonly IUnitOfWork _unitOfWork;

    private readonly IMapper _mapper;
    public GetSlotByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<IEnumerable<SlotDetailsDTO>> Handle(GetSlotByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await Task.FromResult(_unitOfWork.Users.GetAll().FirstOrDefault(u => u.Name == request.Name));
        if (user == null)
            {
                throw new EntityNotFoundException($"No USER found for {request.Name}");
            }

        var slots = await _unitOfWork.Slot.GetSlotByUserId(user.UserId);
        if (slots == null|| !slots.Any())
            {
                throw new EntityNotFoundException($"No Slot found for {user.Name}");
            }
            return _mapper.Map<IEnumerable<SlotDetailsDTO>>(slots);
    }
}   
