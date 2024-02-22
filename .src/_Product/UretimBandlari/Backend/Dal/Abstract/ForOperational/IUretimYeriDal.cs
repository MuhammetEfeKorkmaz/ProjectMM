using Entities.DbModels.ProductModels;
using FullSharedCore.DataAccess.Abstract;

namespace Dal.Abstract.ForOperational
{
    public interface IUretimYeriDal : IRepository<UretimYeri>
    {
        public Task<UretimYeri> GetByName(string _param, CancellationToken token);
    }
}
