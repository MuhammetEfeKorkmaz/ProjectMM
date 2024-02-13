using Microsoft.EntityFrameworkCore;

namespace FullSharedCore.DataAccess.Abstract
{
    public interface IBaseUnitOfWork: IDisposable
    {
        public void SaveChanges(Exception e);
        public Task SaveChangesAsync(Exception e);
    }
}
