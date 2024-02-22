using Dal.Abstract.ForOperational;
using Entities.DbModels.ProductModels;
using FullSharedCore.DataAccess.Concrete;
using Microsoft.EntityFrameworkCore;

namespace Dal.Concrete.ForOperational
{
    public class TestUrunSeriBazindaDal : Repository<TestUrunSeriBazinda>, ITestUrunSeriBazindaDal { public TestUrunSeriBazindaDal(DbSet<TestUrunSeriBazinda> dbset) : base(dbset) { } }
   
}
