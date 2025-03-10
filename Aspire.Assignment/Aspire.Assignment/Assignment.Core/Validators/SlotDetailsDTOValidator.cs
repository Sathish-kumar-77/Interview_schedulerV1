using System;
using Assignment.Contracts.DTO;
using FluentValidation;

namespace Assignment.Core.Validators;

public class SlotDetailsDTOValidator : AbstractValidator<SlotDetailsDTO>
{

    public SlotDetailsDTOValidator()
    {
        
            RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId is required");
            
        
    }

}
