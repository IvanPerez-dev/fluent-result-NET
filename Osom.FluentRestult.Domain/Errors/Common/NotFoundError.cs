using FluentResults;

namespace Osom.FluentRestult.Domain.Errors.Common
{
    public sealed class NotFoundError : Error
    {
        public NotFoundError(string message)
            : base(message)
        {
            Metadata.Add("ErrorCode", "NotFound");
        }
    }
}
