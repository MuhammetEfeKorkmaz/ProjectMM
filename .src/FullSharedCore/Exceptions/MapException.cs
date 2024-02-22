namespace FullSharedCore.Exceptions
{
    public class MapException : Exception
    {
        public MapException(string? message = "", Exception? exp = null) : base(message: !string.IsNullOrEmpty(message) ? message : "Api içerisinde Model Mapleme üzerinde hata oluştu.", innerException: exp) { }
    }

}
