using FluentResults;

namespace Osom.FluentRestult.Domain.Errors.Common
{
    public sealed class ConflictError : DomainError
    {
        public ConflictError(string message)
            : base(message, "CONFLICT")
        {
            //Metadata.Add(MetadataKeys.ErrorCode, "CONFLICT");
        }
    }
}
