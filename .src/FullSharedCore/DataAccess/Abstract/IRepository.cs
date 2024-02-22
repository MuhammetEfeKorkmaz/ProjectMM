using FullSharedCore.Extensions.Predicates;
using FullSharedResults.BaseModels;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FullSharedCore.DataAccess.Abstract
{

    public interface IRepository<T> where T : _EntitiyBaseModel
    {
        DbSet<T> Table { get; }

        void Add(T entity); 
        void Add<Tmap>(Tmap tmap) where Tmap : _DtoBaseModel;
         

        Task AddAsync(T entity); 
        Task AddAsync<Tmap>(Tmap tmap) where Tmap : _DtoBaseModel;
         

        Task AddRange(IList<T> entities); 
        Task AddRange<Tmap>(IList<Tmap> tmaps) where Tmap : _DtoBaseModel;




        Task Update(T entity); 
        Task Update<Tmap>(Tmap tmap) where Tmap : _DtoBaseModel; 

        Task UpdateRange(IList<T> entities); 
        Task UpdateRange<Tmap>(IList<Tmap> tmaps) where Tmap : _DtoBaseModel;




        Task Delete(T entity, DbStatusEnum statusEnum); 
        Task Delete<Tmap>(Tmap tmap, DbStatusEnum statusEnum) where Tmap : _DtoBaseModel; 
        Task Delete(int id, DbStatusEnum statusEnum); 
        Task DeleteRange(IList<T> entities, DbStatusEnum statusEnum); 
        Task DeleteRange<Tmap>(IList<Tmap> tmaps, DbStatusEnum statusEnum) where Tmap : _DtoBaseModel;



        Task DeleteHard(T entity); 
        Task DeleteHard<Tmap>(Tmap tmap) where Tmap : _DtoBaseModel; 
        Task DeleteHard(int id); 
        Task DeleteRangeHard(IList<T> entities); 
        Task DeleteRangeHard<Tmap>(IList<Tmap> tmaps) where Tmap : _DtoBaseModel;





        Task<T> GetById(int id, DbStatusEnum statusEnum); 
        Task<bool> AnyAsync(Expression<Func<T, bool>> filter); 
        IQueryable<T> Where(Expression<Func<T, bool>> filter = null);



        Task<List<T>> GetAll(DbStatusEnum statusEnum); 
        Task<List<Tmap>> GetAll<Tmap>(DbStatusEnum statusEnum) where Tmap : _DtoBaseModel;




        IIncludableJoin<T, TProp> Join<TProp>(Expression<Func<T, TProp>> navigationProperty);
    }
}
