using Castle.DynamicProxy;
using FullSharedCore.Aspects.Base;
using FullSharedCore.Aspects.Secured.Extensions;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Authentication;


namespace FullSharedCore.Aspects.Secured
{
    public class SecuredOperationAspect : InterceptionBaseAttiribute
    {
        private string[] _roles;
        private IActionContextAccessor _httpContextAccessor;

        public SecuredOperationAspect(string roles)
        {
            _roles = roles.Split(',');
            _httpContextAccessor = ServiceRegistiration_FullSharedCore.ServiceProvider.GetService<IActionContextAccessor>();
        }
      

        public override void OnBefore(IInvocation invocation)
        {
            var roleClaims = _httpContextAccessor.ActionContext.HttpContext.User.ClaimRoles();
            foreach (var role in _roles)
            {
                if (roleClaims.Contains(role))
                    return;
            }


            var methodName = string.Format($"{invocation.Method.ReflectedType.FullName}.{invocation.Method.Name}");
            var arguments = invocation.Arguments.Where(x=>!x.GetType().Equals(typeof(CancellationToken))).ToList();
            var key = $"{methodName}({string.Join(",", arguments.Select(x => x?.ToString() ?? "<Null>"))})";
            throw new AuthenticationException("Yetkisiz İşlem", new AuthenticationException(key));
            
        }
    }
}
