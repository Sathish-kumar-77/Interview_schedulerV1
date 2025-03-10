using System;
using Assignment.Contracts.Data;
using Assignment.Core.Exceptions;
using MediatR;

namespace Assignment.Core.Handlers.Commands;

public class UpdateSlotStatusCommand : IRequest<bool>
{
    public int SlotId { get; }
    public string Status { get; }
    public UpdateSlotStatusCommand(int sloId,string status)
    {
        SlotId = sloId;
        Status = status;
    }

}


public class UpdateSlotStatusCommandHandler : IRequestHandler<UpdateSlotStatusCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;
    public UpdateSlotStatusCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<bool> Handle(UpdateSlotStatusCommand request, CancellationToken cancellationToken)
    {
        var slot = await Task.FromResult(_unitOfWork.Slot.Get(request.SlotId));
        if(slot == null)
        {
            throw new EntityNotFoundException($"Slot with ID {request.SlotId} not found.");
        }

        if (slot.Status == request.Status)
            {
               throw new InvalidOperationException($"Slot {slot.SlotId} is already marked as {slot.Status}");
            }

            slot.Status = request.Status;
            _unitOfWork.Slot.Update(slot);
             await _unitOfWork.CommitAsync();
             return true;
    }
}
