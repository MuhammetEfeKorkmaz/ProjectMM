using DTOs.ForOperational;
using DTOs.ProductModels;
using FullSharedResults.Results;
using Newtonsoft.Json;

namespace WebUI.Site._Data
{
    public class TestServisi
    {
        private readonly HttpClient httpClient;
        public TestServisi(HttpClient _httpClient)
        {
            httpClient = _httpClient;
        }


        public async Task<DtoNormalIstasyon> TestIcinIstasyonGetir(string urunSeri, string istasyonId)
        {
            var HttpResult = await httpClient.MyGetFromJsonAsync<DataResult<DtoNormalIstasyon>>($"api/OperationManagement/TestIcinIstasyonGetir?param={urunSeri}&param2={istasyonId}", CancellationToken.None);
            if (HttpResult is null)
            {
                throw new Exception("Api Bağlantı Sorunu Oluştu. Veri Gelmedi");
            }

            if (!HttpResult.Success)
            {
                throw new Exception("Api içerisinde Sorun Oluştu. Veri Gelmedi");
            }

            return HttpResult.Data;
        }


        public async Task<DtoKararIstasyonu> TestIcinKararIstasyonuGetir(string urunSeri, string istasyonId)
        {
            var HttpResult = await httpClient.MyGetFromJsonAsync<DataResult<DtoKararIstasyonu>>($"api/OperationManagement/TestIcinKararIstasyonGetir?param={urunSeri}&param2={istasyonId}", CancellationToken.None);
            if (HttpResult is null)
            {
                throw new Exception("Api Bağlantı Sorunu Oluştu. Veri Gelmedi");
            }

            if (!HttpResult.Success)
            {
                throw new Exception("Api içerisinde Sorun Oluştu. Veri Gelmedi");
            }

            return HttpResult.Data;
        }


        public async Task<List<DtoNormalIstasyonSonucOzeti>> TestIcinIstasyonuYukle(DtoNormalIstasyon param)
        {
            var HttpResult = await httpClient.MyPostFromJsonAsync<DataResult<List<DtoNormalIstasyonSonucOzeti>>>($"api/OperationManagement/TestIcinIstasyonuYukle", JsonConvert.SerializeObject(param), CancellationToken.None);
            if (HttpResult is null)
            {
                throw new Exception("Api Bağlantı Sorunu Oluştu. Veri Gelmedi");
            }

            if (!HttpResult.Success)
            {
                throw new Exception("İşleminiz Gerçekleşmedi.");
            }

            return HttpResult.Data;



        }




    }
}
