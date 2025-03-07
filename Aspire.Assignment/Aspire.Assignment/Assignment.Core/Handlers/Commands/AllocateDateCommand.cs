using System;
using Assignment.Contracts.Data;
using Assignment.Contracts.Data.Entities;
using Assignment.Contracts.DTO;
using Assignment.Core.Exceptions;
using FluentValidation;
using MediatR;

namespace Assignment.Core.Handlers.Commands;

public class AllocateDateCommand : IRequest<string>
{
    public AllocateDateDTO Model { get; }

    public AllocateDateCommand(AllocateDateDTO model)
    {
        this.Model = model;
    }
}

public class AllocateDateCommandHandler : IRequestHandler<AllocateDateCommand, string>
{
    private readonly IUnitOfWork _unitOfWork;

    public AllocateDateCommandHandler(
        IUnitOfWork unitOfWork

    )
    {
        _unitOfWork = unitOfWork;

    }

    public async Task<string> Handle(AllocateDateCommand request, CancellationToken cancellationToken
    )
    {
        AllocateDateDTO model = request.Model;

        bool overlapCheck = await _unitOfWork.PanelCoordinator.CheckStartDateAsync(
           model.StartDate,
           model.PanelMemberID
       );
        if (!overlapCheck)
        {
            throw new DuplicateUserException($"Cannot allocate the same date range{model.StartDate}-{model.EndDate} again for the same panel member{model.PanelMemberID}.");
        }
        else
        {

            var slot = new AllocateDate
            {
                PanelMemberID = model.PanelMemberID,
                StartDate = model.StartDate,
                EndDate = model.EndDate
            };

            await _unitOfWork.PanelCoordinator.AddAllocationAsync(slot);
            await _unitOfWork.CommitAsync();
            return "Allocation added successfully";
        }
    }
}

