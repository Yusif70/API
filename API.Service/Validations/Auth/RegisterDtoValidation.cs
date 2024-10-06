using API.Service.Dtos.Auth;
using FluentValidation;

namespace API.Service.Validations.Auth
{
    public class RegisterDtoValidation : AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidation()
        {
            RuleFor(r => r.Username)
                .NotEmpty()
                .NotNull()
                .MaximumLength(50);
            RuleFor(r => r.Email)
                .NotEmpty()
                .NotNull()
                .EmailAddress();
            RuleFor(r => r.Password)
                .NotEmpty()
                .NotNull()
                .MaximumLength(20);
            RuleFor(r => r.ConfirmPassword)
                .NotEmpty()
                .NotNull()
                .Equal(r => r.Password).WithMessage("Passwords do not match");
        }
    }
}
