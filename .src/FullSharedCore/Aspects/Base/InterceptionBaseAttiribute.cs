using Castle.DynamicProxy;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace FullSharedCore.Aspects.Base
{
    [AttributeUsageAttribute(AttributeTargets.Parameter | AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public abstract class InterceptionBaseAttiribute : Attribute, IInterceptor
    {
        public int Priority { get; set; }
     
        public virtual void OnBefore(IInvocation invocation) { }
        public virtual void OnException(IInvocation invocation, Exception e) { }
        public virtual void OnSuccess(IInvocation invocation) { }
        protected virtual void OnAfter(IInvocation invocation) { }



        public virtual void Intercept(IInvocation invocation)
        {
            OnBefore(invocation);

            invocation.Proceed();

            //Async Methodlar için
            var method = invocation.MethodInvocationTarget;
            var isAsync = method.GetCustomAttribute(typeof(AsyncStateMachineAttribute)) != null;
            if (isAsync && typeof(Task).IsAssignableFrom(method.ReturnType))
            {
                var task = invocation.ReturnValue as Task;
                if (task != null && task.IsFaulted)
                {
                    throw new Exception(task.Exception.Message, task.Exception);
                }
                invocation.ReturnValue = InterceptAsync((dynamic)invocation.ReturnValue);
            }

            OnAfter(invocation);
        }



        public  async Task InterceptAsync(Task task)
        {
            try
            {
                await task.ConfigureAwait(false);
            }
            catch (OperationCanceledException ex)
            {
                throw new OperationCanceledException(ex.Message, ex.CancellationToken);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

        }
        public async Task<T> InterceptAsync<T>(Task<T> task)
        {
            try
            {
                T result = await task.ConfigureAwait(false);
                return result;
            }
            catch (OperationCanceledException ex)
            {
                throw new OperationCanceledException(ex.Message, ex.CancellationToken);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }



}
