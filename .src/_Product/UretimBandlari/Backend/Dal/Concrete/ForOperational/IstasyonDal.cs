using Dal.Abstract.ForOperational;
using Entities.DbModels.ProductModels;
using FullSharedCore.DataAccess.Concrete;
using Microsoft.EntityFrameworkCore;

namespace Dal.Concrete.ForOperational
{
    public class IstasyonDal : Repository<Istasyon>, IIstasyonDal { public IstasyonDal(DbSet<Istasyon> dbset) : base(dbset) { } }

}
