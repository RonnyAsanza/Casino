namespace Casino.Application.Wrappers
{
    public class Response<T>
    {
        public Response()
        {
        }
        public Response(T data, string message = "")
        {
            Succeeded = true;
            Message = message;
            Data = data;
        }
        public Response(string message, bool succeeded)
        {
            Succeeded = succeeded;
            Message = message;
        }
        public Response(string message, IEnumerable<string> errors)
        {
            Succeeded = false;
            Message = message;
            Errors = errors;
        }
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        //public Dictionary<string, string[]> Errors { get; set; } = new();
        public IEnumerable<string> Errors { get; set; }

        public T Data { get; set; }
    }
}
