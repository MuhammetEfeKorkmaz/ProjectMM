namespace FullSharedCore.DataAccess.Abstract
{
    public interface IBaseUnitOfWorkQuery : IDisposable
    {
        public void SaveChanges(Exception e);
        Task SaveChangesAsync(Exception e);
    }
}
