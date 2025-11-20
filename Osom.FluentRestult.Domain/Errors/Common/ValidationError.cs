using FluentResults;

namespace Osom.FluentRestult.Domain.Errors.Common
{
    public sealed class ValidationError : Error
    {
        public ValidationError(string message)
            : base(message)
        {
            Metadata.Add("ErrorCode", "ValidationError");
        }
    }
}
