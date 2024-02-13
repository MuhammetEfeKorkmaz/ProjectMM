using Entities.DbModels.UserModels;
using FullSharedCore.DataAccess.Abstract;

namespace Dal.Abstract.ForUser
{
    public interface ISystemUserDal : IRepository<SystemUser> {
        public Task<SystemUser> GetByMail(string _param, CancellationToken token);


    }
}
