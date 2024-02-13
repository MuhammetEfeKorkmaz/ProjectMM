using Castle.Core.Internal;
using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FullSharedCore.Aspects.Base
{
    public class InterceptorCollection : IInterceptorSelector
    {
        public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
        {
            
            var classAttributes = type.GetCustomAttributes<InterceptionBaseAttiribute>(true).ToList();
            var methodAttributes = type.GetMethod(method.Name).GetCustomAttributes<InterceptionBaseAttiribute>(true);
            classAttributes.AddRange(methodAttributes);
            string DeveloperKritikUyariMesaji = $"!!!Lütfen Dikkat!!!{Environment.NewLine}{Assembly.GetEntryAssembly().GetName().Name}.{type.FullName}.{method.Name}{Environment.NewLine}" +
                $"isimli Busines Servis methodunda aynı Priority değeri işaretlenmiş Aspect Attribute bulunmaktadır.{Environment.NewLine} Method Güvenlik nedeniyle Kullanıcıya Dönüş yapmamıştır.";
            
            
            bool securtyThrow= false;
            foreach (var attribute in classAttributes.GroupBy(x => x.Priority).Where(y => y.Count() > 1))
            {
                securtyThrow = true;
                DeveloperKritikUyariMesaji = DeveloperKritikUyariMesaji + $"{Environment.NewLine}{string.Join(',', attribute.Select(x=>x.GetType().Name+ " Priority:" + x.Priority))}";
            }


            if (securtyThrow)
            {
                throw new BadImageFormatException(DeveloperKritikUyariMesaji);
            }




           

            return classAttributes.OrderBy(x => x.Priority).ToArray();
        }
    }
}
