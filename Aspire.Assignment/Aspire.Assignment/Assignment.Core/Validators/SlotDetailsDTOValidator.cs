using System;
using Assignment.Contracts.DTO;
using FluentValidation;

namespace Assignment.Core.Validators;

public class SlotDetailsDTOValidator : AbstractValidator<SlotDetailsDTO>
{

    public SlotDetailsDTOValidator()
    {
         RuleFor(x => x.SlotId).NotEmpty().WithMessage("SlotId is required");
            RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId is required");
            RuleFor(x => x.Date).NotEmpty().WithMessage("Date is required")
             .GreaterThanOrEqualTo(DateTime.Today).WithMessage("Date should be today or a future date");;
            RuleFor(x => x.StartTime).NotEmpty().WithMessage("StartTime is required");
            RuleFor(x => x.EndTime).NotEmpty().WithMessage("EndTime is required");
            RuleFor(x => x.Status).NotEmpty().WithMessage("Status is required");
        
    }

}
