using FluentResults;

namespace Osom.FluentRestult.Domain.Errors.Common
{
    public sealed class ConflictError : Error
    {
        public ConflictError(string message)
            : base(message)
        {
            Metadata.Add("ErrorCode", "Conflict");
        }
    }
}
