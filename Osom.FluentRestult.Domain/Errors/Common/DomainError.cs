using FluentResults;

namespace Osom.FluentRestult.Domain.Errors.Common
{
    public class DomainError : Error
    {
        public string ErrorCode { get; set; }

        public DomainError(string message, string errorCode = "BUSINESS_ERROR")
            : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}
