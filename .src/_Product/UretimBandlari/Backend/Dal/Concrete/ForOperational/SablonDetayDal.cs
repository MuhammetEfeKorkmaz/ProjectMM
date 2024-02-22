using Dal.Abstract.ForOperational;
using Entities.DbModels.ProductModels;
using FullSharedCore.DataAccess.Concrete;
using Microsoft.EntityFrameworkCore;

namespace Dal.Concrete.ForOperational
{
    public class SablonDetayDal : Repository<SablonDetay>, ISablonDetayDal { public SablonDetayDal(DbSet<SablonDetay> dbset) : base(dbset) { } }

}
