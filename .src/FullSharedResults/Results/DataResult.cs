using Newtonsoft.Json;

namespace FullSharedResults.Results
{
    public class DataResult<T> : Result, IDataResult<T>
    {
        //  [JsonConstructor]
        public DataResult(T data, bool success = false, string message = "") : base(success, message)
        {
            Data = data;
        }
        [JsonConstructor]
        public DataResult(T data, bool success = false) : base(success)
        {
            Data = data;
        }

        public T Data { get; }
    }
}
