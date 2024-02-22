using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Collections;
using System.Linq.Expressions;

namespace FullSharedCore.Extensions.Predicates
{
    public interface IIncludableJoin<out T, out TProp> : IQueryable<T>
    {
    }
    public class IncludableJoin<T, TPrevProp> : IIncludableJoin<T, TPrevProp>
    {
        private readonly IIncludableQueryable<T, TPrevProp> _query;

        public IncludableJoin(IIncludableQueryable<T, TPrevProp> query)
        {
            _query = query;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _query.GetEnumerator();
        }

        public Expression Expression => _query.Expression;
        public Type ElementType => _query.ElementType;
        public IQueryProvider Provider => _query.Provider;

        internal IIncludableQueryable<T, TPrevProp> GetQuery()
        {
            return _query;
        }
    }


    public static class RepositoryExtensions
    {
        public static IIncludableJoin<T, TProp> Join<T, TProp>(this IQueryable<T> query, Expression<Func<T, TProp>> propToExpand) where T : class
        {
            return new IncludableJoin<T, TProp>(query.Include(propToExpand));
        }

        public static IIncludableJoin<T, TProp> ThenJoin<T, TPrevProp, TProp>(this IIncludableJoin<T, TPrevProp> query, Expression<Func<TPrevProp, TProp>> propToExpand) where T : class
        {
            IIncludableQueryable<T, TPrevProp> queryable = ((IncludableJoin<T, TPrevProp>)query).GetQuery();
            return new IncludableJoin<T, TProp>(queryable.ThenInclude(propToExpand));
        }

        public static IIncludableJoin<T, TProp> ThenJoin<T, TPrevProp, TProp>(this IIncludableJoin<T, IEnumerable<TPrevProp>> query, Expression<Func<TPrevProp, TProp>> propToExpand) where T : class
        {
            var queryable = ((IncludableJoin<T, IEnumerable<TPrevProp>>)query).GetQuery();
            var include = queryable.ThenInclude(propToExpand);
            return new IncludableJoin<T, TProp>(include);
        }
        public static IIncludableJoin<T, TProp> ThenJoin<T, TPrevProp, TProp>(this IIncludableJoin<T, ICollection<TPrevProp>> query, Expression<Func<TPrevProp, TProp>> propToExpand) where T : class
        {
            var queryable = ((IncludableJoin<T, ICollection<TPrevProp>>)query).GetQuery();
            var include = queryable.ThenInclude(propToExpand);
            return new IncludableJoin<T, TProp>(include);
        }




        public static IQueryable<T> IncludeMultiple<T>(this IQueryable<T> query, params Expression<Func<T, object>>[] includes) where T : class
        {
            if (includes != null)
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            return query;
        }
    }
}
