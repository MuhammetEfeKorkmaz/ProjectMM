using Castle.DynamicProxy;
using FullSharedCore.Aspects.Base;
using Microsoft.Extensions.DependencyInjection;
using ServiceStack.Redis;

namespace FullSharedCore.Aspects.Caching
{
    public class CacheAspect : InterceptionBaseAttiribute
    {
        private ICacheManager _cacheManager_MM;
        private IRedisClient _cacheManager_Redis;
        private CacheType _type;

        public CacheAspect(CacheType type)
        {
            _type = type;
            if (type == CacheType.Redis)
            {
                _cacheManager_Redis = ServiceRegistiration_FullSharedCore.ServiceProvider.GetService<IRedisClient>();
            }
            else if (type == CacheType.MicrosoftMemoryCache)
            {
                _cacheManager_MM = ServiceRegistiration_FullSharedCore.ServiceProvider.GetService<ICacheManager>();
            }

        }
     
        public override async void Intercept(IInvocation invocation)
        {
            if (_type == CacheType.Redis)
            {

                // Redise Gidecek.

            }
            else if (_type == CacheType.MicrosoftMemoryCache)
            {
                var methodName = string.Format($"{invocation.Method.ReflectedType.FullName}.{invocation.Method.Name}");
                var arguments = invocation.Arguments.ToList();
                var key = $"{methodName}({string.Join(",", arguments.Select(x => x?.ToString() ?? "<Null>"))})";
                if (_cacheManager_MM.IsAdd(key))
                {
                    invocation.ReturnValue = _cacheManager_MM.Get(key);
                    return;
                }
                invocation.Proceed();
                _cacheManager_MM.Add(key, invocation.ReturnValue);
            }

        }


    }
}
