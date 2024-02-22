using Castle.DynamicProxy;
using FullSharedCore.Aspects.Base;
using FullSharedCore.DataAccess.Abstract;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace FullSharedCore.Aspects.Transaction
{

    public class TransactionAspectQueryDb : InterceptionBaseAttiribute
    {

        public IBaseUnitOfWorkQuery uow;

        public TransactionAspectQueryDb()
        {
            uow = ServiceRegistiration_FullSharedCore.ServiceProvider.GetService<IBaseUnitOfWorkQuery>();
        }


        public override void Intercept(IInvocation invocation)
        {
            var isSuccess = true;
            try
            {
                invocation.Proceed();

                // Async Methodlar için
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



            }
            catch (Exception e)
            {
                isSuccess = false;
                baseOp(invocation, e);
                throw;
            }

            finally
            {
                if (isSuccess)
                    baseOp(invocation, null);
            }

        }



        private void baseOp(IInvocation invocation, Exception e)
        {
            var method = invocation.MethodInvocationTarget;
            var isAsync = method.GetCustomAttribute(typeof(AsyncStateMachineAttribute)) != null;
            if (isAsync && typeof(Task).IsAssignableFrom(method.ReturnType))
            {
                var task = invocation.ReturnValue as Task;
                if (task != null && task.IsFaulted)
                    uow?.SaveChanges(e);
                else
                    invocation.ReturnValue = InterceptAsync((dynamic)invocation.ReturnValue, e);
            }
            else
                uow?.SaveChanges(e);
        }
        private async Task InterceptAsync(Task task, Exception e)
        {
            if (task is not null)
            {
                await task.ConfigureAwait(false);
                await uow?.SaveChangesAsync(e);
            }
            else
            {

            }

        }
        private async Task<T> InterceptAsync<T>(Task<T> task, Exception e)
        {
            if (task is not null)
            {
                T result = await task.ConfigureAwait(false);
                await uow?.SaveChangesAsync(e);
                return result;
            }
            else
            {
                throw new Exception();
            }

        }
    }
}
