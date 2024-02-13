using Castle.DynamicProxy;
using FluentValidation;
using FullSharedCore.Aspects.Base;
using FullSharedCore.Utilities.Constants.Message;

namespace FullSharedCore.Aspects.Validation
{
    public class ValidationAspect : InterceptionBaseAttiribute
    {



        private Type _validatorType;
        private ValidatorMethodType _validatorMethodType = ValidatorMethodType.Default;
        public ValidationAspect(Type validatorType, ValidatorMethodType validatorMethodType = ValidatorMethodType.Default)
        {
            if (!typeof(IValidator).IsAssignableFrom(validatorType))
            {
                throw new Exception(SystemMessages.UserMessages.ForUser.Aspect.Validation.OlumsuzDogrulama);
            }
            _validatorType = validatorType;
            _validatorMethodType = validatorMethodType;
        }
        public ValidationAspect(Type validatorType)
        {
            if (!typeof(IValidator).IsAssignableFrom(validatorType))
            {
                throw new Exception(SystemMessages.UserMessages.ForUser.Aspect.Validation.OlumsuzDogrulama);
            }
            _validatorType = validatorType;
        }




        public override void OnBefore(IInvocation invocation)
        {
            IValidator validator = default;
            int? ctorsParam= _validatorType.GetConstructors(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)?.FirstOrDefault()?.GetParameters()?.Count();
            ctorsParam= ctorsParam ?? 0;
            if (ctorsParam > 0)
            {
                validator = (IValidator)Activator.CreateInstance(_validatorType, _validatorMethodType);
            }
            else
            {
                validator = (IValidator)Activator.CreateInstance(_validatorType);
            }


            var entityType = _validatorType.BaseType.GetGenericArguments()[0];
            var entities = invocation.Arguments.Where(t => t.GetType() == entityType);
            foreach (var entity in entities)
            {
                ValidationTool.Validate(validator, entity);
            }
        }
    }
}
