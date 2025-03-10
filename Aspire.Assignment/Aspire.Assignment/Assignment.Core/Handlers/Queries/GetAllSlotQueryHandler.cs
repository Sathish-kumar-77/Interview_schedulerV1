using System;
using Assignment.Contracts.Data;
using Assignment.Contracts.DTO;
using MediatR;

namespace Assignment.Core.Handlers.Queries;

public class GetAllSlotQuery : IRequest<IEnumerable<SlotDetailsDTO>> { }

public class GetAllSlotQueryHandler : IRequestHandler<GetAllSlotQuery, IEnumerable<SlotDetailsDTO>>
{
    private readonly IUnitOfWork _repository;

    public GetAllSlotQueryHandler(IUnitOfWork repository)
    {
        _repository = repository;
    }


    public async Task<IEnumerable<SlotDetailsDTO>> Handle(GetAllSlotQuery request, CancellationToken cancellationToken)
    {

        var slots = await _repository.Slot.GetAllAsync();

        var slotDTOs = new List<SlotDetailsDTO>();

        foreach (var slot in slots)
        {
            slotDTOs.Add(
                new SlotDetailsDTO
                {
                    UserId = slot.UserId,
                    Date = slot.Date,
                    StartTime = slot.StartTime,
                    EndTime = slot.EndTime,
                    Status = slot.Status
                }
            );
        }

        return slotDTOs;
    }
}
