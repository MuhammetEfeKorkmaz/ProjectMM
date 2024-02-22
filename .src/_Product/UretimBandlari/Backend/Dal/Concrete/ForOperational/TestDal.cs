using Dal.Abstract.ForOperational;
using Entities.DbModels.ProductModels;
using FullSharedCore.DataAccess.Concrete;
using Microsoft.EntityFrameworkCore;

namespace Dal.Concrete.ForOperational
{
    public class TestDal : Repository<Test>, ITestDal { public TestDal(DbSet<Test> dbset) : base(dbset) { } }
 
}
