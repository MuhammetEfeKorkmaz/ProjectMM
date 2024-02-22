using Dal.Abstract.ForOperational;
using Entities.DbModels.ProductModels;
using FullSharedCore.DataAccess.Concrete;
using Microsoft.EntityFrameworkCore;

namespace Dal.Concrete.ForOperational
{
    public class TestUrunBazindaDal : Repository<TestUrunBazinda>, ITestUrunBazindaDal { public TestUrunBazindaDal(DbSet<TestUrunBazinda> dbset) : base(dbset) { } }
  
}
