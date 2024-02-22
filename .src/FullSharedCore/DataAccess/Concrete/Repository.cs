using AutoMapper;
using FullSharedCore.DataAccess.Abstract;
using FullSharedCore.Exceptions;
using FullSharedCore.Extensions.Predicates;
using FullSharedResults.BaseModels;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq.Expressions;

namespace FullSharedCore.DataAccess.Concrete
{


    public class Repository<T> : IRepository<T> where T : _EntitiyBaseModel
    {
        private CancellationToken? token { get => ServiceRegistiration_FullSharedCore.ServiceProvider.GetService<IHttpContextAccessor>()?.HttpContext?.RequestAborted; }

        private IMapper _mapper;
        private IMapper mapper { get => _mapper ?? (_mapper = ServiceRegistiration_FullSharedCore.ServiceProvider.GetService<IMapper>()); }
        private T mapSingle<Tmap>(Tmap tmap) where Tmap : _DtoBaseModel
        {
            T entity = mapper.Map<Tmap, T>(tmap);
            if (entity is null)
                throw new MapException();
            return entity;
        }
        private Tmap mapRevesedSingle<Tmap>(T entitiy) where Tmap : _DtoBaseModel
        {
            Tmap tmap = mapper.Map<T, Tmap>(entitiy);
            if (tmap is null)
                throw new MapException();
            return tmap;
        }
        private IList<T> mapPlural<Tmap>(IList<Tmap> tmap) where Tmap : _DtoBaseModel
        {
            IList<T> entity = mapper.Map<IList<Tmap>, IList<T>>(tmap);
            if (entity is null)
                throw new MapException();
            return entity;
        }
        private List<Tmap> mapRevesedPlural<Tmap>(List<T> entities) where Tmap : _DtoBaseModel
        {
            List<Tmap> tmaps = mapper.Map<List<T>, List<Tmap>>(entities);
            if (tmaps is null)
                throw new MapException();
            return tmaps;
        }

        public DbSet<T> Table { get; }
        public Repository(DbSet<T> table)
        {
            this.Table = table;
        }


        public void Add(T entity)
        {
            Table.Add(entity);
        }
        public void Add<Tmap>(Tmap tmap) where Tmap : _DtoBaseModel
        {
            Table.Add(mapSingle(tmap));
        }

        public async Task AddAsync(T entity)
        {
            if (token == null) await Table.AddAsync(entity);
            else await Table.AddAsync(entity, token.Value);
        }
        public async Task AddAsync<Tmap>(Tmap tmap) where Tmap : _DtoBaseModel
        {
            if (token == null) await Table.AddAsync(mapSingle(tmap));
            else await Table.AddAsync(mapSingle(tmap), token.Value);
        }

        public async Task AddRange(IList<T> entities)
        {
            if (token == null) await Table.AddRangeAsync(entities);
            else await Table.AddRangeAsync(entities, token.Value);
        }
        public async Task AddRange<Tmap>(IList<Tmap> tmaps) where Tmap : _DtoBaseModel
        {
            if (token == null) await Table.AddRangeAsync(mapPlural(tmaps));
            else await Table.AddRangeAsync(mapPlural(tmaps), token.Value);
        }


        public async Task Update(T entity)
        {
            if (token == null) await Task.Run(() => Table.Update(entity)).ConfigureAwait(false);
            else await Task.Run(() => Table.Update(entity), token.Value).ConfigureAwait(false);
        }
        public async Task Update<Tmap>(Tmap tmap) where Tmap : _DtoBaseModel
        {
            if (token == null) await Task.Run(() => Table.Update(mapSingle(tmap))).ConfigureAwait(false);
            else await Task.Run(() => Table.Update(mapSingle(tmap)), token.Value).ConfigureAwait(false);
        }

        public async Task UpdateRange(IList<T> entities)
        {
            if (token == null) await Task.Run(() => Table.UpdateRange(entities)).ConfigureAwait(false);
            else await Task.Run(() => Table.UpdateRange(entities), token.Value).ConfigureAwait(false);
        }
        public async Task UpdateRange<Tmap>(IList<Tmap> tmaps) where Tmap : _DtoBaseModel
        {
            if (token == null) await Task.Run(() => Table.UpdateRange(mapPlural(tmaps))).ConfigureAwait(false);
            else await Task.Run(() => Table.UpdateRange(mapPlural(tmaps)), token.Value).ConfigureAwait(false);
        }


        public async Task Delete(T entity, DbStatusEnum statusEnum)
        {
            entity.Status = statusEnum.GetDbStatus();
            if (token == null) await Task.Run(() => Table.Update(entity)).ConfigureAwait(false);
            else await Task.Run(() => Table.Update(entity), token.Value).ConfigureAwait(false);
        }
        public async Task Delete<Tmap>(Tmap tmap, DbStatusEnum statusEnum) where Tmap : _DtoBaseModel
        {
            T entity = mapSingle(tmap);
            entity.Status = statusEnum.GetDbStatus();
            if (token == null) await Task.Run(() => Table.Update(entity)).ConfigureAwait(false);
            else await Task.Run(() => Table.Update(entity), token.Value).ConfigureAwait(false);
        }

        public async Task Delete(int id, DbStatusEnum statusEnum)
        {
            T? row = default;
            if (token == null) row = await Table.FindAsync(id);
            else row = await Table.FindAsync(id, token.Value);

            if (row is not null)
            {
                row.Status = statusEnum.GetDbStatus();
                if (token == null) await Task.Run(() => Table.Remove(row)).ConfigureAwait(false);
                else await Task.Run(() => Table.Remove(row), token.Value).ConfigureAwait(false);
            }

        }

        public async Task DeleteRange(IList<T> entities, DbStatusEnum statusEnum)
        {
            foreach (T item in entities)
                item.Status = statusEnum.GetDbStatus();

            if (token == null) await Task.Run(() => Table.RemoveRange(entities)).ConfigureAwait(false);
            else await Task.Run(() => Table.RemoveRange(entities), token.Value).ConfigureAwait(false);
        }
        public async Task DeleteRange<Tmap>(IList<Tmap> tmaps, DbStatusEnum statusEnum) where Tmap : _DtoBaseModel
        {
            IList<T> entities = mapPlural(tmaps);
            foreach (T item in entities)
                item.Status = statusEnum.GetDbStatus();
            if (token == null) await Task.Run(() => Table.RemoveRange(entities)).ConfigureAwait(false);
            else await Task.Run(() => Table.RemoveRange(entities), token.Value).ConfigureAwait(false);
        }

        public async Task DeleteHard(T entity)
        {
            if (token == null) await Task.Run(() => Table.Remove(entity)).ConfigureAwait(false);
            else await Task.Run(() => Table.Remove(entity), token.Value).ConfigureAwait(false);
        }
        public async Task DeleteHard<Tmap>(Tmap tmap) where Tmap : _DtoBaseModel
        {
            if (token == null) await Task.Run(() => Table.Remove(mapSingle(tmap))).ConfigureAwait(false);
            else await Task.Run(() => Table.Remove(mapSingle(tmap)), token.Value).ConfigureAwait(false);
        }

        public async Task DeleteHard(int id)
        {
            T? row = default;
            if (token == null) row = await Table.FindAsync(id);
            else row = await Table.FindAsync(id, token.Value);

            if (row is not null)
            {
                if (token == null) await Task.Run(() => Table.Remove(row)).ConfigureAwait(false);
                else await Task.Run(() => Table.Remove(row), token.Value).ConfigureAwait(false);
            }

        }
        public async Task DeleteRangeHard(IList<T> entities)
        {
            if (token == null) await Task.Run(() => Table.RemoveRange(entities)).ConfigureAwait(false);
            else await Task.Run(() => Table.RemoveRange(entities), token.Value).ConfigureAwait(false);
        }
        public async Task DeleteRangeHard<Tmap>(IList<Tmap> tmaps) where Tmap : _DtoBaseModel
        {
            if (token == null) await Task.Run(() => Table.RemoveRange(mapPlural(tmaps))).ConfigureAwait(false);
            else await Task.Run(() => Table.RemoveRange(mapPlural(tmaps)), token.Value).ConfigureAwait(false);
        }



        public async Task<T> GetById(int id, DbStatusEnum statusEnum)
        {
            return token is null ? await Table.Where(x => x.Status == statusEnum.GetDbStatus()).FirstOrDefaultAsync(x => x.Id == id) : await Table.Where(x => x.Status == statusEnum.GetDbStatus()).FirstOrDefaultAsync(x => x.Id == id, token.Value);
        }



        public async Task<bool> AnyAsync(Expression<Func<T, bool>> filter)
        {
            return token is null ? await Table.AsNoTracking().AnyAsync(filter) : await Table.AsNoTracking().AnyAsync(filter, token.Value);
        }
        public IQueryable<T> Where(Expression<Func<T, bool>> filter = null)
        {
            return filter == null
                ? Table.AsNoTracking()
                : Table.AsNoTracking().Where(filter);

        }

        public async Task<List<T>> GetAll(DbStatusEnum statusEnum)
        {
            return token is null ? await Table.Where(x => x.Status == statusEnum.GetDbStatus()).ToListAsync() : await Table.Where(x => x.Status == statusEnum.GetDbStatus()).ToListAsync(token.Value);
        }
        public async Task<List<Tmap>> GetAll<Tmap>(DbStatusEnum statusEnum) where Tmap : _DtoBaseModel
        {
            var result = token is null ? await Table.Where(x => x.Status == statusEnum.GetDbStatus()).ToListAsync() : await Table.Where(x => x.Status == statusEnum.GetDbStatus()).ToListAsync(token.Value);
            return mapRevesedPlural<Tmap>(result);
        }



        public IIncludableJoin<T, TProp> Join<TProp>(Expression<Func<T, TProp>> navigationProperty)
        {
            return ((IQueryable<T>)Table).Join(navigationProperty);
        }


    }






}
