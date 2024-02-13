using DTOs.UserModels;
using FluentValidation;
using FullSharedCore.Aspects.Validation;

namespace Business.ValidationRules.ForUser
{
    public class RegisterValidator : AbstractValidator<SystemUserAddUpdateDto>
    {
        
        public RegisterValidator(ValidatorMethodType _validatorType)
        {
            if (_validatorType == ValidatorMethodType.Add)
            {
                RuleFor(c => c.Name).NotEmpty().WithMessage("Araç isim alanı boş geçilemez.");
                RuleFor(c => c.Nick).NotEmpty().WithMessage("Araç isim alanı boş geçilemez.");
                RuleFor(c => c.Password).NotEmpty().WithMessage("Araç isim alanı boş geçilemez.");
                RuleFor(c => c.Email).NotEmpty().WithMessage("Email boş geçilemez.").EmailAddress().WithMessage("Email adres formatı hatalı.");
                RuleFor(c => c.OperationClaimsID).NotEmpty().WithMessage("Araç isim alanı boş geçilemez.");
                RuleForEach(x=>x.OperationClaimsID).NotNull().Must((x,claimId)=> !claimId.Equals(0) & claimId>0).WithMessage("Araç isim alanı boş geçilemez.");
            }
            else if (_validatorType == ValidatorMethodType.Update)
            {
                RuleFor(c => c.Name).NotEmpty().WithMessage("Araç isim alanı boş geçilemez.");
                RuleFor(c => c.Nick).NotEmpty().WithMessage("Araç isim alanı boş geçilemez.");
                RuleFor(c => c.Password).NotEmpty().WithMessage("Araç isim alanı boş geçilemez.");
                RuleFor(c => c.Email).NotEmpty().WithMessage("Araç isim alanı boş geçilemez.");
                RuleForEach(x => x.OperationClaimsID).NotNull().Must((x, claimId) => !claimId.Equals(0) & claimId > 0).WithMessage("Araç isim alanı boş geçilemez.");
            }
          
        }
    }

   


}
