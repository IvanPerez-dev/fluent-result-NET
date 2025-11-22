using FluentResults;

namespace Osom.FluentRestult.Domain.Errors.Common
{
    public sealed class ValidationError : DomainError
    {
        public ValidationError(string message)
            : base(message, "VALIDATION_ERROR")
        {
            //Metadata.Add(MetadataKeys.ErrorCode, "VALIDATION_ERROR");
        }
    }
}
