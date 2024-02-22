using Castle.DynamicProxy;
using FullSharedCore.Aspects.Base;
using FullSharedCore.DataAccess.Abstract;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace FullSharedCore.Aspects.Transaction
{
    public class TransactionAspectCommandDb : InterceptionBaseAttiribute
    {

        private readonly IBaseUnitOfWorkCommand uow;
        private readonly bool IsForcedSaveChanges;
        public TransactionAspectCommandDb(bool IsForcedSaveChanges = true)
        {
            this.IsForcedSaveChanges = IsForcedSaveChanges;
            uow = ServiceRegistiration_FullSharedCore.ServiceProvider.GetService<IBaseUnitOfWorkCommand>();
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
                {
                    if (IsForcedSaveChanges)
                        uow?.SaveChanges(e);
                }
                else
                    invocation.ReturnValue = InterceptAsync((dynamic)invocation.ReturnValue, e);
            }
            else
            {
                if (IsForcedSaveChanges)
                    uow?.SaveChanges(e);
            } 
        }
        private async Task InterceptAsync(Task task, Exception e)
        {
            if (task is not null)
            {
                await task.ConfigureAwait(false);
                if (IsForcedSaveChanges)
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
                if (IsForcedSaveChanges)
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
