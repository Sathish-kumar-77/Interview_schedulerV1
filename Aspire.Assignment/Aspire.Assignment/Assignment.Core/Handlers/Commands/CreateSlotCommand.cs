using System;
using Assignment.Contracts.Data;
using Assignment.Contracts.Data.Entities;
using Assignment.Contracts.DTO;
using Assignment.Core.Exceptions;
using FluentValidation;
using MediatR;

namespace Assignment.Core.Handlers.Commands;

public class CreateSlotCommand : IRequest<string>
{
    public SlotDetailsDTO Model { get; }
    public CreateSlotCommand(SlotDetailsDTO model)
    {
        Model = model;
    }
}

public class CreateSlotCommandHandler : IRequestHandler<CreateSlotCommand, string>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<SlotDetailsDTO> _validator;

    public CreateSlotCommandHandler(IUnitOfWork unitOfWork, IValidator<SlotDetailsDTO> validator)
    {
        _validator = validator;
        _unitOfWork = unitOfWork;

    }

    public async Task<string> Handle(CreateSlotCommand request, CancellationToken cancellationToken)
    {

        SlotDetailsDTO model = request.Model;
        var users = await Task.FromResult(_unitOfWork.Users.GetAll().FirstOrDefault(u => u.UserId == model.UserId));
        if (users == null)
        {
            throw new EntityNotFoundException("Panel member not found.");
        }

        var allocateDate = await Task.FromResult(_unitOfWork.AllocateDate.GetAll().Where(u => u.PanelMemberID == users.UserId).ToList());
        if (allocateDate.Count == 0)
        {
            throw new EntityNotFoundException("No allocation dates found for the user.");
        }

         var result = _validator.Validate(model);
        if (!result.IsValid)
        {
            var errors = result.Errors.Select(x => x.ErrorMessage).ToArray();
            throw new InvalidRequestBodyException { Errors = errors };
        }

        var validDate = allocateDate.Any(ad => model.Date >= ad.StartDate && model.Date <= ad.EndDate);
        if (!validDate)
        {
            throw new DuplicateUserException(
                $"Entered date {model.Date} is not within any allocation date range."
            );
        }

        bool overlapCheck = await _unitOfWork.Slot.CheckDateAsync(model.StartTime, model.EndTime, model.UserId, model.Date);
        if (overlapCheck)
        {
            throw new DuplicateUserException($"Cannot allocate the same Time range{model.StartTime}-{model.EndTime} again For Date{model.Date}");
        }
        else
        {
            var entity = new Slot
            {
                UserId = model.UserId,
                Date = model.Date,
                StartTime = model.StartTime,
                EndTime = model.EndTime,
                Status = model.Status
            };
            await _unitOfWork.Slot.createSlotAsync(entity);
            await _unitOfWork.CommitAsync();
            return "Allocation added successfully";
        }
    }
}
