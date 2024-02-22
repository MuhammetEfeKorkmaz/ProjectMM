using Dal.Abstract.ForOperational;
using Entities.DbModels.ProductModels;
using FullSharedCore.DataAccess.Concrete;
using Microsoft.EntityFrameworkCore;

namespace Dal.Concrete.ForOperational
{
    public class UretimYeriDal : Repository<UretimYeri>, IUretimYeriDal 
    {
        DbSet<UretimYeri> dbset;
        public UretimYeriDal(DbSet<UretimYeri> _dbset) : base(_dbset) 
        {
            dbset= _dbset;
        } 
        public async Task<UretimYeri> GetByName(string _param, CancellationToken token)
        {
            return await dbset.FirstOrDefaultAsync(x => x.Adi.Equals(_param), token);
        }
    }

}
