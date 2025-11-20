using FluentResults;

namespace Osom.FluentRestult.Domain.Errors.Common
{
    public sealed class ForbiddenError : Error
    {
        public ForbiddenError(string message)
            : base(message)
        {
            Metadata.Add("ErrorCode", "Forbidden");
        }
    }
}
