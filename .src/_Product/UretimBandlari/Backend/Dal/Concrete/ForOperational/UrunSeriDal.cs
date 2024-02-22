using Dal.Abstract.ForOperational;
using Entities.DbModels.ProductModels;
using FullSharedCore.DataAccess.Concrete;
using Microsoft.EntityFrameworkCore;

namespace Dal.Concrete.ForOperational
{
    public class UrunSeriDal : Repository<UrunSeri>, IUrunSeriDal { public UrunSeriDal(DbSet<UrunSeri> dbset) : base(dbset) { } }

}
