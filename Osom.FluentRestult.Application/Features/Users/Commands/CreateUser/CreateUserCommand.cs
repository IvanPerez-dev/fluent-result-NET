using FluentResults;
using MediatR;

namespace Osom.FluentRestult.Application.Features.Users.Commands.CreateUser
{
    public record struct CreateUserCommand(
        string FirstName,
        string LastName,
        string Email,
        string Password
    ) : IRequest<Result<CreateUserResponse>>;
}
