using Dal.Abstract.ForOperational;
using Entities.DbModels.ProductModels;
using FullSharedCore.DataAccess.Concrete;
using Microsoft.EntityFrameworkCore;

namespace Dal.Concrete.ForOperational
{
    public class BantDal : Repository<Bant>, IBantDal {  public BantDal(DbSet<Bant> dbset) : base(dbset) { } }
}
