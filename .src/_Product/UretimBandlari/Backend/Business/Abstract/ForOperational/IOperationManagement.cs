using DTOs.ForOperational;
using DTOs.ProductModels;
using FullSharedResults.Results;

namespace Business.Abstract.ForOperational
{
    public interface IOperationManagement
    {

        public Task<IDataResult<DtoNormalIstasyon>> TestIcinIstasyonGetir(string urunSeri, string istasyonId, CancellationToken token);
        public Task<IDataResult<List<DtoNormalIstasyonSonucOzeti>>> TestIcinIstasyonuYukle(DtoNormalIstasyon dtoNormalIstasyon, CancellationToken token);
        public Task<IDataResult<DtoKararIstasyonu>> TestIcinKararIstasyonGetir(string urunSeri, string istasyonId, CancellationToken token);
      
       
    }
}
