using FullSharedCore.SharedModels.BaseModels;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FullSharedCore.DataAccess.Abstract
{
    public interface IRepository<T> where T : _EntitiyBaseModel
    {
        DbSet<T> Table { get; }
        Task Add(T entity, CancellationToken? token = null);
        void Add(T entity);


        Task AddRange(IList<T> entities, CancellationToken? token = null);
        Task Update(T entity, CancellationToken? token = null);
        Task UpdateRange(IList<T> entities, CancellationToken? token = null);
        Task Delete(T entity, CancellationToken? token = null);
        Task Delete(int id, CancellationToken? token = null);
        Task DeleteRange(IList<T> entities, CancellationToken? token = null);

         
        Task<T> GetById(int id, CancellationToken? token);
        Task<bool> AnyAsync(Expression<Func<T, bool>> filter, CancellationToken? token = null);
        IQueryable<T> Where(Expression<Func<T, bool>> filter);
        Task<List<T>> GetAll(CancellationToken? token = null);
    }
}
