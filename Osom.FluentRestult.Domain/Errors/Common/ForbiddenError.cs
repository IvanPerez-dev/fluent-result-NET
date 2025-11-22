using FluentResults;

namespace Osom.FluentRestult.Domain.Errors.Common
{
    public sealed class ForbiddenError : DomainError
    {
        public ForbiddenError(string message)
            : base(message, "FORBIDDEN")
        {
            //Metadata.Add(MetadataKeys.ErrorCode, "FORBIDDEN");
        }
    }
}
