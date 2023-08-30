namespace Casino.Application
{
    public class ApplicationException : Exception
    {
        public string Type { get; }
        public string Detail { get; }
        public string Title { get; }
        public string Instace { get; }

        protected ApplicationException(string type, string detail, string title, string instance)
            : base(detail)
        {
            Type = type;
            Detail = detail;
            Title = title;
            Instace = instance;
        }
    }
}
