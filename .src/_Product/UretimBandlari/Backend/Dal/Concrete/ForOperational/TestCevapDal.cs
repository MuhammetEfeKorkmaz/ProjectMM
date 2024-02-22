using Dal.Abstract.ForOperational;
using Entities.DbModels.ProductModels;
using FullSharedCore.DataAccess.Concrete;
using Microsoft.EntityFrameworkCore;

namespace Dal.Concrete.ForOperational
{
    public class TestCevapDal : Repository<TestCevap>, ITestCevapDal { public TestCevapDal(DbSet<TestCevap> dbset) : base(dbset) { } }
  
}
