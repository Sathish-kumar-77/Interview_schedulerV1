using FluentValidation;
using Assignment.Contracts.DTO;

namespace Assignment.Core.Validators
{
    public class UpdateUsersDTOValidator : AbstractValidator<UpdateUsersDTO>
    {
        public UpdateUsersDTOValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(100).WithMessage("Name must be at most 100 characters.")
                .When(x => !string.IsNullOrEmpty(x.Name)); 

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email format")
                .When(x => !string.IsNullOrEmpty(x.Email));

            RuleFor(x => x.Password)
                .MinimumLength(6).WithMessage("Password must be at least 6 characters.")
                .When(x => !string.IsNullOrEmpty(x.Password));
        }
    }
}
