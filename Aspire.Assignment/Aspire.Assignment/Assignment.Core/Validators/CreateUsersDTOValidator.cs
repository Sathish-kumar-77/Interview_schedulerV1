using System;
using Assignment.Contracts.DTO;
using FluentValidation;

namespace Assignment.Core.Validators;


public class CreateusersDTOValidator : AbstractValidator<CreateUsersDTO>
    {
        public CreateusersDTOValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Provide passsword");
        }
    }
