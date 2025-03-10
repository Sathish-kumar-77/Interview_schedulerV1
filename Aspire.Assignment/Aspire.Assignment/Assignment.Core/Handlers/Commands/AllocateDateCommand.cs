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
    private readonly IValidator<AllocateDateDTO> _validator;

    public AllocateDateCommandHandler(IUnitOfWork unitOfWork,IValidator<AllocateDateDTO> validator )
    {
        _unitOfWork = unitOfWork;
        _validator = validator;

    }

    public async Task<string> Handle(AllocateDateCommand request, CancellationToken cancellationToken)
    {
        AllocateDateDTO model = request.Model;

        var user = await Task.FromResult(_unitOfWork.Users.GetAll().FirstOrDefault(u => u.UserId == model.PanelMemberID));
        if (user == null)
            {
                throw new EntityNotFoundException($"No USER found for {model.PanelMemberID}");
            }

         var result = _validator.Validate(model);
            if (!result.IsValid)
            {
                var errors = result.Errors.Select(x => x.ErrorMessage).ToArray();
                // Throw InvalidRequestBodyException with the appropriate message
               throw new InvalidRequestBodyException { Errors = errors };
            }

        bool overlapCheck = await _unitOfWork.PanelCoordinator.CheckStartDateAsync(
           model.StartDate, 
           model.PanelMemberID
           );
        if (!overlapCheck)
        {
            throw new InvalidOperationException($"Cannot allocate the same date range{model.StartDate}-{model.EndDate} again for the same panel member{model.PanelMemberID}.");
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

