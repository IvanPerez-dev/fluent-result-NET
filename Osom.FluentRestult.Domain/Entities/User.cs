using FluentResults;
using Osom.FluentRestult.Domain.Errors;

namespace Osom.FluentRestult.Domain.Entities
{
    public sealed class User : Entity<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        private User() { }

        private User(string firstName, string lastName, string email, string password)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
        }

        public static Result<User> Create(
            string firstName,
            string lastName,
            string email,
            string password
        )
        {
            if (string.IsNullOrWhiteSpace(firstName))
            {
                return Result.Fail<User>("First name is required");
            }

            if (string.IsNullOrWhiteSpace(lastName))
            {
                return Result.Fail<User>("Last name is required");
            }

            return Result.Ok(new User(firstName, lastName, email, password));
        }
    }
}
