namespace FullSharedCore.Exceptions
{
    public class HttpClientException : Exception
    {
        public HttpClientException(string? message = "", Exception? exp = null) : base(message: !string.IsNullOrEmpty(message) ? message : "Api içerisinde HttpClient üzerinde hata oluştu.", innerException: exp) { }
    }
}
