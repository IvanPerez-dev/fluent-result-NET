using FluentResults;

namespace Osom.FluentRestult.Domain.Errors.Common
{
    public sealed class NotFoundError : DomainError
    {
        public NotFoundError(string message)
            : base(message, "NOT_FOUND")
        {
            //Metadata.Add(MetadataKeys.ErrorCode, "NOT_FOUND");
        }
    }
}
