using DTOs.UserModels;
using FluentValidation;

namespace Business.ValidationRules.ForUser
{
    public class LoginValidator : AbstractValidator<UserForLoginDto>
    {
        public LoginValidator()
        {

            RuleFor(u => u.Email).NotEmpty().WithMessage("Email boş geçilemez.").EmailAddress().WithMessage("Email adres formatı hatalı.");
            RuleFor(u => u.Password).NotEmpty().WithMessage("Şifre boş geçilemez.");
        }
    }



}
