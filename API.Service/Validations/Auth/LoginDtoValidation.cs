using API.Service.Dtos.Auth;
using FluentValidation;

namespace API.Service.Validations.Auth
{
    public class LoginDtoValidation : AbstractValidator<LoginDto>
    {
        public LoginDtoValidation()
        {
            RuleFor(l => l.UserNameOrEmail)
                .NotEmpty()
                .NotNull();
            RuleFor(l => l.Password)
                .NotEmpty()
                .NotNull();
        }
    }
}
