using Microsoft.AspNetCore.Http;

namespace Osom.FluentRestult.Domain.Exceptions
{
    public class NotFoundException : DomainException
    {
        public override string Title => "Resource not found";
        public override int StatusCode => StatusCodes.Status404NotFound;
        public override string Type => "https://tools.ietf.org/html/rfc7231#section-6.5.4";

        public NotFoundException(string message)
            : base(message) { }
    }
}
