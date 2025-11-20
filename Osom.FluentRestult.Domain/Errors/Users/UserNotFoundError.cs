using FluentResults;

namespace Osom.FluentRestult.Domain.Errors.Users
{
    public sealed class UserNotFoundError : Error
    {
        public UserNotFoundError(int userId)
            : base($"User {userId} not found")
        {
            Metadata.Add("ErrorCode", "UserNotFound");
        }
    }
}
