using Osom.FluentRestult.Application.Interfaces;

namespace Osom.FluentRestult.Application.Features.Users.Commands.CreateUser
{
    public record struct CreateUserCommand(
        string FirstName,
        string LastName,
        string Email,
        string Password
    ) : ICommand<CreateUserResponse>;
}
