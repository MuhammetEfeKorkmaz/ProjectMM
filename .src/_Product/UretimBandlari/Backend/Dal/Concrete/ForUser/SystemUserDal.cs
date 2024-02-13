using Dal.Abstract.ForUser;
using Entities.DbModels.UserModels;
using FullSharedCore.DataAccess.Concrete;
using FullSharedCore.Extensions.Predicates;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using static FullSharedCore.Extensions.Predicates.FilterPredicate;

namespace Dal.Concrete.ForUser
{
    public class SystemUserDal : Repository<SystemUser>, ISystemUserDal
    {
        private DbSet<SystemUser> dbset;// { get => RepositoryGet.GetDbSet<SystemUser, PDbContext>(); }

        public SystemUserDal(DbSet<SystemUser> dbset) : base(dbset) { this.dbset = dbset; }

        public async Task<SystemUser> GetByMail(string _param, CancellationToken token)
        { 
            return await dbset.Include(x => x.OperationClaimss).FirstOrDefaultAsync(x => x.Email.Equals(_param), token);
        }

        public async Task<SystemUser> GetById(string _param, CancellationToken token)
        {
            return await dbset.Include(x => x.OperationClaimss).FirstOrDefaultAsync(x => x.Id.Equals(_param), token);
        }
        public async Task<SystemUser> GetByIds(string _param, CancellationToken token)
        {
            return await dbset.Include(x => x.OperationClaimss).FirstOrDefaultAsync(x => x.Id.Equals(_param), token);
        }



    }
}


