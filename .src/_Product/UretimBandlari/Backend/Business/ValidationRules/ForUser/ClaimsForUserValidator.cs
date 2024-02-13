using DTOs.UserModels;
using FluentValidation;
using FullSharedCore.Aspects.Validation;

namespace Business.ValidationRules.ForUser
{
    public class ClaimsForUserValidator : AbstractValidator<OperationClaimAddUpdateForSystemUserDto>
    {
        public ClaimsForUserValidator(ValidatorMethodType validatorMethodType)
        {
            RuleFor(u => u.SystemUserFID).NotEmpty().Must((x, FID) => !FID.Equals(0) & FID > 0).WithMessage("SystemUserFID Sıfırdan büyük olmalı");
            RuleFor(u => u.OperationClaimsFID).NotEmpty().Must((x, FID) => !FID.Equals(0) & FID > 0).WithMessage("OperationClaimsFID Sıfırdan büyük olmalı");
            if (validatorMethodType == ValidatorMethodType.Delete)
            {
                RuleFor(u => u.Id).NotEmpty().WithMessage("Email boş geçilemez.");
            }


        }
    }
}
