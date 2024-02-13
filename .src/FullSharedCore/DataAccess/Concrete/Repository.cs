using FullSharedCore.DataAccess.Abstract;
using FullSharedCore.Extensions;
using FullSharedCore.SharedModels.BaseModels;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FullSharedCore.DataAccess.Concrete
{


    public class Repository<T> : IRepository<T> where T : _EntitiyBaseModel
    { 
        public  DbSet<T> Table { get; }
        public Repository(DbSet<T> table)
        {
            this.Table = table;
        }


        public void Add(T entity)
        {
            Table.Add(entity);
        }
        public async Task Add(T entity, CancellationToken? token)
        {
            if (token == null) await Table.AddAsync(entity);
            else await Table.AddAsync(entity, token.Value);
        }

   



        public async Task AddRange(IList<T> entities, CancellationToken? token)
        {
            if (token == null) await Table.AddRangeAsync(entities);
            else await Table.AddRangeAsync(entities, token.Value);
        }
        public async Task Update(T entity, CancellationToken? token)
        {
            if (token == null) await Task.Run(() => Table.Update(entity)).ConfigureAwait(false); 
            else  await Task.Run(() => Table.Update(entity), token.Value).ConfigureAwait(false);

        }
        public async Task UpdateRange(IList<T> entities, CancellationToken? token)
        {
            if (token == null) await Task.Run(() => Table.UpdateRange(entities)).ConfigureAwait(false);
            else await Task.Run(() => Table.UpdateRange(entities), token.Value).ConfigureAwait(false);
        }
        public async Task Delete(T entity, CancellationToken? token)
        {
            if (token == null) await Task.Run(() => Table.Remove(entity)).ConfigureAwait(false);
            else await Task.Run(() => Table.Remove(entity), token.Value).ConfigureAwait(false); 
        }
        public async Task Delete(int id, CancellationToken? token)
        {
            T? row = default;
            if (token == null) row= await Table.FindAsync(id);
            else row = await Table.FindAsync(id, token.Value);
             
            if (row is not null)
            {
                if (token == null) await Task.Run(() => Table.Remove(row)).ConfigureAwait(false);
                else await Task.Run(() => Table.Remove(row),token.Value).ConfigureAwait(false);
            }
               
        }
        public async Task DeleteRange(IList<T> entities, CancellationToken? token = null)
        { 
            if (token == null) await Task.Run(() => Table.RemoveRange(entities)).ConfigureAwait(false);
            else await Task.Run(() => Table.RemoveRange(entities), token.Value).ConfigureAwait(false);
        }



        //public async Task<T> GetByIds<T,R>(List<int> ids, Expression<Func<T, R>> selector, CancellationToken? token)
        //{
        //    List<int> Idler = new List<int>() { 10, 11, 12, 13, 14 };
        //    var pre = Idler.PredicateGetIds((SystemUser u) => u.Id);

        //    return token is null ? await Table.FindAsync(id) : await Table.FindAsync(id, token.Value);
        //}
        public async Task<T> GetById(int id, CancellationToken? token)
        {
            return token is null ? await Table.FindAsync(id) : await Table.FindAsync(id, token.Value);
        }
        public async Task<bool> AnyAsync(Expression<Func<T, bool>> filter, CancellationToken? token)
        {
            return token is null ? await Table.AsNoTracking().AnyAsync(filter) : await Table.AsNoTracking().AnyAsync(filter, token.Value);
        }
        public IQueryable<T> Where(Expression<Func<T, bool>> filter)
        {
            return filter == null
                ? Table.AsNoTracking()
                : Table.AsNoTracking().Where(filter);

        }
        public async Task<List<T>> GetAll(CancellationToken? token)
        {
            return token is null ? await Table.ToListAsync() : await Table.ToListAsync(token.Value);
        }

       
    }
}
