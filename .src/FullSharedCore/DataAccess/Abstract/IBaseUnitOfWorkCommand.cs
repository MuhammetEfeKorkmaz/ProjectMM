using Microsoft.EntityFrameworkCore;

namespace FullSharedCore.DataAccess.Abstract
{


    public interface IBaseUnitOfWorkCommand : IDisposable
    { 
        public void SaveChanges(Exception e);
        Task SaveChangesAsync(Exception e);
    }
  
}
