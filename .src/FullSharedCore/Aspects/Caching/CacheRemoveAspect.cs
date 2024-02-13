using Castle.DynamicProxy;
using FullSharedCore.Aspects.Base;
using Microsoft.Extensions.DependencyInjection;

namespace FullSharedCore.Aspects.Caching
{
    public class CacheRemoveAspect : InterceptionBaseAttiribute
    {
        private string _pattern;
        private ICacheManager _cacheManager;
        public CacheRemoveAspect(string pattern)
        {
            _pattern = pattern;
            _cacheManager = ServiceRegistiration_FullSharedCore.ServiceProvider.GetService<ICacheManager>();
        }

     
        public override void OnBefore(IInvocation invocation)
        {
            _cacheManager.RemoveByPattern(_pattern);
        }


    }
}
