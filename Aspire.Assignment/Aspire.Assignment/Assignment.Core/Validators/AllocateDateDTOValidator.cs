using System;
using Assignment.Contracts.Data.Entities;
using Assignment.Contracts.DTO;
using FluentValidation;

namespace Assignment.Core.Validators;

public class AllocateDateDTOValidator : AbstractValidator<AllocateDateDTO>
{
    public AllocateDateDTOValidator()
        {
            RuleFor(x => x.PanelMemberID).NotEmpty().WithMessage("PanelMemberId is required");
            RuleFor(x => x.StartDate).NotEmpty().WithMessage("Provide the start date for the panel member slot")
            .GreaterThanOrEqualTo(DateTime.Today).WithMessage("Date should be today or a future date");;
            RuleFor(x => x.EndDate).NotEmpty().WithMessage("Enddate is required to complete the process");
        }
}
