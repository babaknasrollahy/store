namespace Store.Common
{
    public class ResultDTO
    {
        public bool isSuccess { get; set; }
        public string Message { get; set; }
    }

    public class ResultDTO<T>
    {
        public bool isSuccess { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
