using Dal.Abstract;
using Dal.Abstract.ForUser;
using Dal.Concrete.ForUser;
using Entities.DbModels.UserModels;
using FullSharedCore;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Dal.Concrete
{

    public class UnitOfWork : IUnitOfWork, IDisposable
    { 
        private PDbContext Context;
    
        public UnitOfWork(PDbContext _context) 
        { 
            Context = _context;
            systemUserDal = new SystemUserDal(Context.Set<SystemUser>());
            operationClaimsDal = new OperationClaimsDal(Context.Set<OperationClaims>()); 
        }
      
        public ISystemUserDal systemUserDal { get; private set; }
        public IOperationClaimsDal operationClaimsDal { get; private set; } 


        private CancellationToken? token { get => ServiceRegistiration_FullSharedCore.ServiceProvider.GetService<IHttpContextAccessor>()?.HttpContext?.RequestAborted; }
        public void SaveChanges(Exception e)
        {
            if (token is not null)
            {
                if (!token.Value.IsCancellationRequested)
                {
                    if (e is null)
                       // if (Context.Database.CurrentTransaction is not null)
                            Context.SaveChanges();
                }
            }
            else
            {
                if (e is null)
                   // if (Context.Database.CurrentTransaction is not null)
                        Context.SaveChanges();
            } 
        }
         
        public async Task SaveChangesAsync(Exception e)
        {
            if (token is not null)
            {
                if (!token.Value.IsCancellationRequested)
                {
                    if (e is null)
                        //if (Context.Database is not null)
                            await Context.SaveChangesAsync(token.Value);
                }
            }
            else
            {
                if (e is null)
                    //if (Context.Database.CurrentTransaction is not null)
                        await Context.SaveChangesAsync();
            } 
        }

        

        public async void Dispose()
        {
            if (Context is not null)
            {
                await Context.DisposeAsync();
                Context = null;
            }
           
        }
    }
}
