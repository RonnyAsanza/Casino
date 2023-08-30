namespace Casino.Application.Exceptions
{
    public class NotFoundException : ApplicationException
    {
        public NotFoundException(string instance, string details)
            : base("https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.4", details, "Not found", instance)
        {
        }
    }
}
