using FluentResults;
using MediatR;

namespace Osom.FluentRestult.Application.Features.Users.Commands.DeleteUser
{
    public record DeleteUserCommand(int Id) : IRequest<Result> { }
}
