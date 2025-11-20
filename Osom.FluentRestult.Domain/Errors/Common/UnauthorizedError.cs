using FluentResults;

namespace Osom.FluentRestult.Domain.Errors.Common
{
    public sealed class UnauthorizedError : Error
    {
        public UnauthorizedError(string message)
            : base(message)
        {
            Metadata.Add("ErrorCode", "Unauthorized");
        }
    }
}
