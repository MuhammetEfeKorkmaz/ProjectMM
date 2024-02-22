using Dal.Abstract.ForUser;
using Entities.DbModels.UserModels;
using FullSharedCore.DataAccess.Concrete;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Dal.Concrete.ForUser
{
    public class SystemUserDal : Repository<SystemUser>, ISystemUserDal  { public SystemUserDal(DbSet<SystemUser> dbset) : base(dbset) {}  }



    


   
}


