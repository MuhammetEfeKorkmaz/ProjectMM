using FullSharedResults.Results;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using IResult = FullSharedResults.Results.IResult;

namespace WebUI.Site._Data
{
    public static class _MyBaseHttpService
    {
        public static async Task<T> MyGetFromJsonAsync<T>(this HttpClient client, string url, CancellationToken token)
        {
            try
            {
                var result = await client.GetAsync(url, token);
                if (!result.IsSuccessStatusCode)
                {
                    throw new Exception("Api Sorunu Oluştu.");
                }
                return JsonConvert.DeserializeObject<T>(await result.Content.ReadAsStringAsync(token));
            }
            catch (Exception ex)
            {
                throw new Exception("Bir Sorun Oluştu." + Environment.NewLine + "Ek Bilgi:" + ex.Message);
            }


        }

        public static async Task<IResult> MyPostAsync<T>(this HttpClient client, string url, T modelJson, CancellationToken token)
        {
            try
            { 
                StringContent stringContent = new StringContent(JsonConvert.SerializeObject(modelJson), Encoding.UTF8, "application/json");
                 
                var result = await client.PostAsync(url, stringContent, token);
                if (!result.IsSuccessStatusCode)
                {
                    throw new Exception("Api Sorunu Oluştu.");
                }
                return JsonConvert.DeserializeObject<Result>(await result.Content.ReadAsStringAsync(token));
            }
            catch (Exception ex)
            {
                throw new Exception("Bir Sorun Oluştu." + Environment.NewLine + "Ek Bilgi:" + ex.Message);
            }
        }


        public static async Task<T> MyPostFromJsonAsync<T>(this HttpClient client, string url, string modelJson, CancellationToken token)
        {
            try
            {
                StringContent stringContent = new StringContent(modelJson, Encoding.UTF8, "application/json");

                var result = await client.PostAsync(url, stringContent, token);
                if (!result.IsSuccessStatusCode)
                {
                    throw new Exception("Api Sorunu Oluştu.");
                }
                return JsonConvert.DeserializeObject<T>(await result.Content.ReadAsStringAsync(token));
            }
            catch (Exception ex)
            {
                throw new Exception("Bir Sorun Oluştu." + Environment.NewLine + "Ek Bilgi:" + ex.Message);
            }
        }


    }
}
