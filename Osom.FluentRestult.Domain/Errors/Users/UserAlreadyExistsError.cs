using FluentResults;

namespace Osom.FluentRestult.Domain.Errors.Users
{
    public sealed class UserAlreadyExistsError : Error
    {
        public UserAlreadyExistsError(string email)
            : base($"User {email} already exists")
        {
            Metadata.Add("ErrorCode", "UserAlreadyExists");
        }
    }
}
