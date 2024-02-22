using FullSharedCore.Exceptions;
using FullSharedResults.Results;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using HttpClient = System.Net.Http.HttpClient;

namespace FullSharedCore.Utilities.HttpClientOperations
{

    public interface IHttpClientOperation
    {
        public string BaseUrl { get; set; }
        Task<T> GetModel<T>(string url = "", string param = "");
       
        Task<T> GetModel<T>(string url = "", string param = "", string token = "");

        Task<IDataResult<T>> GetDataResult<T>(string url = "", string param = "");

        Task<IDataResult<T>> GetDataResult<T>(string url = "", string param = "", string token = "");

        Task<IResult> GetResult(string url = "", string param = "");

        Task<IResult> GetResult(string url = "", string param = "", string token = "");




        Task<T> PostModel<T>(T model, string url = "");

        Task<TResult> PostModel<T, TResult>(T model, string url = "");

        Task<T> PostModel<T>(T model, string url = "", string token = "");

        Task<TResult> PostModel<T, TResult>(T model, string url = "", string token = "");

        Task<IDataResult<T>> PostDataResult<T>(T model, string url = "");

        Task<IDataResult<T>> PostDataResult<T>(T model, string url = "", string token = "");

        Task<IDataResult<TResult>> PostDataResult<T, TResult>(T model, string url = "");

        Task<IDataResult<TResult>> PostDataResult<T, TResult>(T model, string url = "", string token = "");




        Task<T> PutModel<T>(T model, string url = "");

        Task<TResult> PutModel<T, TResult>(T model, string url = "");

        Task<T> PutModel<T>(T model, string url = "", string token = "");

        Task<TResult> PutModel<T, TResult>(T model, string url = "", string token = "");

        Task<IDataResult<T>> PutDataResult<T>(T model, string url = "");

        Task<IDataResult<T>> PutDataResult<T>(T model, string url = "", string token = "");

        Task<IDataResult<TResult>> PutDataResult<T, TResult>(T model, string url = "");

        Task<IDataResult<TResult>> PutDataResult<T, TResult>(T model, string url = "", string token = "");



        Task<T> DeleteModel<T>(string url = "", string param = "");

        Task<T> DeleteModel<T>(string url = "", string param = "", string token = "");

        Task<IDataResult<T>> DeleteDataResult<T>(string url = "", string param = "");

        Task<IDataResult<T>> DeleteDataResult<T>(string url = "", string param = "", string token = "");

        Task<IResult> DeleteResult(string url = "", string param = "");

        Task<IResult> DeleteResult(string url = "", string param = "", string token = "");

    }


    public class HttpClientOperation : IHttpClientOperation
    {

        private readonly HttpClient httpClient;

        public string BaseUrl { get; set; }


        public HttpClientOperation(HttpClient _httpClient)
        {
            httpClient = _httpClient;
        }

         


        public async Task<T> GetModel<T>(string url = "", string param = "")
        {
            var result = await baseGetMethod(url, param);
            return JsonConvert.DeserializeObject<T>(await result.Content.ReadAsStringAsync());
        }
        public async Task<T> GetModel<T>(string url = "", string param = "", string token = "")
        {
            var result = await baseGetMethod(url, param, token);
            return JsonConvert.DeserializeObject<T>(await result.Content.ReadAsStringAsync());
        }
        public async Task<IDataResult<T>> GetDataResult<T>(string url = "", string param = "")
        {
            var result = await baseGetMethod(url, param);
            return JsonConvert.DeserializeObject<DataResult<T>>(await result.Content.ReadAsStringAsync());
        }
        public async Task<IDataResult<T>> GetDataResult<T>(string url = "", string param = "", string token = "")
        {
            var result = await baseGetMethod(url, param, token);
            return JsonConvert.DeserializeObject<DataResult<T>>(await result.Content.ReadAsStringAsync());
        }
        public async Task<IResult> GetResult(string url = "", string param = "")
        {
            var result = await baseGetMethod(url, param);
            return JsonConvert.DeserializeObject<FullSharedResults.Results.Result>(await result.Content.ReadAsStringAsync());
        }
        public async Task<IResult> GetResult(string url = "", string param = "", string token = "")
        {
            var result = await baseGetMethod(url, param, token);
            return JsonConvert.DeserializeObject<FullSharedResults.Results.Result>(await result.Content.ReadAsStringAsync());
        }



        public async Task<T> PostModel<T>(T model, string url = "")
        {
            var result = await basePostMethod(model, url);
            return JsonConvert.DeserializeObject<T>(await result.Content.ReadAsStringAsync());
        }
        public async Task<TResult> PostModel<T, TResult>(T model, string url = "")
        {
            var result = await basePostMethod(model, url);
            return JsonConvert.DeserializeObject<TResult>(await result.Content.ReadAsStringAsync());
        }
        public async Task<T> PostModel<T>(T model, string url = "", string token = "")
        {
            var result = await basePostMethod(model, url, token);
            return JsonConvert.DeserializeObject<T>(await result.Content.ReadAsStringAsync());
        }
        public async Task<TResult> PostModel<T, TResult>(T model, string url = "", string token = "")
        {
            var result = await basePostMethod(model, url, token);
            return JsonConvert.DeserializeObject<TResult>(await result.Content.ReadAsStringAsync());
        }
        public async Task<IDataResult<T>> PostDataResult<T>(T model, string url = "")
        {
            var result = await basePostMethod(model, url);
            return JsonConvert.DeserializeObject<DataResult<T>>(await result.Content.ReadAsStringAsync());
        }
        public async Task<IDataResult<T>> PostDataResult<T>(T model, string url = "", string token = "")
        {
            var result = await basePostMethod(model, url, token);
            return JsonConvert.DeserializeObject<DataResult<T>>(await result.Content.ReadAsStringAsync());
        }
        public async Task<IDataResult<TResult>> PostDataResult<T, TResult>(T model, string url = "")
        {
            var result = await basePostMethod(model, url);
            return JsonConvert.DeserializeObject<DataResult<TResult>>(await result.Content.ReadAsStringAsync());
        }
        public async Task<IDataResult<TResult>> PostDataResult<T, TResult>(T model, string url = "", string token = "")
        {
            var result = await basePostMethod(model, url, token);
            return JsonConvert.DeserializeObject<DataResult<TResult>>(await result.Content.ReadAsStringAsync());
        }


        public async Task<T> PutModel<T>(T model, string url = "")
        {
            var result = await basePutMethod(model, url);
            return JsonConvert.DeserializeObject<T>(await result.Content.ReadAsStringAsync());
        }
        public async Task<TResult> PutModel<T, TResult>(T model, string url = "")
        {
            var result = await basePutMethod(model, url);
            return JsonConvert.DeserializeObject<TResult>(await result.Content.ReadAsStringAsync());
        }
        public async Task<T> PutModel<T>(T model, string url = "", string token = "")
        {
            var result = await basePutMethod(model, url, token);
            return JsonConvert.DeserializeObject<T>(await result.Content.ReadAsStringAsync());
        }
        public async Task<TResult> PutModel<T, TResult>(T model, string url = "", string token = "")
        {
            var result = await basePutMethod(model, url, token);
            return JsonConvert.DeserializeObject<TResult>(await result.Content.ReadAsStringAsync());
        }
        public async Task<IDataResult<T>> PutDataResult<T>(T model, string url = "")
        {
            var result = await basePutMethod(model, url);
            return JsonConvert.DeserializeObject<DataResult<T>>(await result.Content.ReadAsStringAsync());
        }
        public async Task<IDataResult<T>> PutDataResult<T>(T model, string url = "", string token = "")
        {
            var result = await basePutMethod(model, url, token);
            return JsonConvert.DeserializeObject<DataResult<T>>(await result.Content.ReadAsStringAsync());
        }
        public async Task<IDataResult<TResult>> PutDataResult<T, TResult>(T model, string url = "")
        {
            var result = await basePutMethod(model, url);
            return JsonConvert.DeserializeObject<DataResult<TResult>>(await result.Content.ReadAsStringAsync());
        }
        public async Task<IDataResult<TResult>> PutDataResult<T, TResult>(T model, string url = "", string token = "")
        {
            var result = await basePutMethod(model, url, token);
            return JsonConvert.DeserializeObject<DataResult<TResult>>(await result.Content.ReadAsStringAsync());
        }





        public async Task<T> DeleteModel<T>(string url = "", string param = "")
        {
            var result = await baseDeleteMethod(url, param);
            return JsonConvert.DeserializeObject<T>(await result.Content.ReadAsStringAsync());
        }
        public async Task<T> DeleteModel<T>(string url = "", string param = "", string token = "")
        {
            var result = await baseDeleteMethod(url, param, token);
            return JsonConvert.DeserializeObject<T>(await result.Content.ReadAsStringAsync());
        }
        public async Task<IDataResult<T>> DeleteDataResult<T>(string url = "", string param = "")
        {
            var result = await baseDeleteMethod(url, param);
            return JsonConvert.DeserializeObject<DataResult<T>>(await result.Content.ReadAsStringAsync());
        }
        public async Task<IDataResult<T>> DeleteDataResult<T>(string url = "", string param = "", string token = "")
        {
            var result = await baseDeleteMethod(url, param, token);
            return JsonConvert.DeserializeObject<DataResult<T>>(await result.Content.ReadAsStringAsync());
        }
        public async Task<IResult> DeleteResult(string url = "", string param = "")
        {
            var result = await baseDeleteMethod(url, param);
            return JsonConvert.DeserializeObject<FullSharedResults.Results.Result>(await result.Content.ReadAsStringAsync());
        }
        public async Task<IResult> DeleteResult(string url = "", string param = "", string token = "")
        {
            var result = await baseDeleteMethod(url, param, token);
            return JsonConvert.DeserializeObject<FullSharedResults.Results.Result>(await result.Content.ReadAsStringAsync());
        }










        private async Task<HttpResponseMessage> baseGetMethod(string url = "", string param = "")
        {
            var baseResult = baseMethod<string>(null, url, param);
            var result = await baseResult.Item1.GetAsync($"{url}/{param}");
            if (!result.IsSuccessStatusCode)
            {
                throw new Exception();
            }
            return result;
        }
        private async Task<HttpResponseMessage> baseGetMethod(string url = "", string param = "", string token = "")
        { 
            var baseResult = baseMethod<string>(null,url, param, token);
            var result = await baseResult.Item1.GetAsync($"{url}/{param}");
            if (!result.IsSuccessStatusCode)
            {
                throw new Exception();
            }
            return result; 
        }
        private async Task<HttpResponseMessage> basePostMethod<T>(T model, string url = "")
        {
            var baseResult = baseMethod(model, url);
            var result = await baseResult.Item1.PostAsync(url, baseResult.Item2);
            if (!result.IsSuccessStatusCode)
            {
                throw new Exception();
            }
            return result;
        }
        private async Task<HttpResponseMessage> basePostMethod<T>(T model, string url = "", string token = "")
        { 
            var baseResult = baseMethod(model, url, token: token);
            var result = await baseResult.Item1.PostAsync(url, baseResult.Item2);
            if (!result.IsSuccessStatusCode)
            {
                throw new Exception();
            }
            return result; 
        }
        private async Task<HttpResponseMessage> basePutMethod<T>(T model, string url = "")
        {
            var baseResult = baseMethod(model, url);
            var result = await baseResult.Item1.PutAsync(url, baseResult.Item2);
            if (!result.IsSuccessStatusCode)
            {
                throw new Exception();
            }
            return result;
        }
        private async Task<HttpResponseMessage> basePutMethod<T>(T model, string url = "", string token = "")
        {
            var baseResult = baseMethod(model,url, token: token);
            var result = await baseResult.Item1.PutAsync(url, baseResult.Item2); 
            if (!result.IsSuccessStatusCode)
            {
                throw new Exception();
            }
            return result;
        }
        private async Task<HttpResponseMessage> baseDeleteMethod(string url = "", string param = "")
        {
            var baseResult = baseMethod<string>(null,url, param);
            var result = await baseResult.Item1.DeleteAsync($"{url}/{param}");
            if (!result.IsSuccessStatusCode)
            {
                throw new Exception();
            }
            return result;
        }
        private async Task<HttpResponseMessage> baseDeleteMethod(string url = "", string param = "", string token = "")
        {

            var baseResult = baseMethod<string>(null, url, param, token);
            var result =await baseResult.Item1.DeleteAsync($"{url}/{param}");
            if (!result.IsSuccessStatusCode)
            {
                throw new Exception();
            }
            return result; 
        }


        private Tuple<HttpClient, StringContent> baseMethod<T>(T model, string url = "", string param = "", string token = "")
        {
            try
            {
                if (string.IsNullOrEmpty(BaseUrl)) throw new HttpClientException($"Eksik Parametre {nameof(BaseUrl)}");
                if (string.IsNullOrEmpty(url)) throw new HttpClientException($"Eksik Parametre {nameof(url)}");
                if (string.IsNullOrEmpty(param)) throw new HttpClientException($"Eksik Parametre {nameof(param)}");


                httpClient.BaseAddress = new Uri(BaseUrl);
                httpClient.DefaultRequestHeaders.Clear();
                if (!string.IsNullOrEmpty(token))
                    httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

                StringContent stringContent = null;
                if (model != null)
                {
                    string modelJson = JsonConvert.SerializeObject(model);
                    stringContent = new StringContent(modelJson, Encoding.UTF8, "application/json");
                }
                else
                {
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                }
                return new Tuple<HttpClient, StringContent>(httpClient, stringContent);
            }
            catch (Exception ex)
            {
                throw new HttpClientException(exp:ex);
            }
           
        }

    }
}
