using FluentResults;
using MediatR;
using Osom.FluentRestult.Domain.Errors.Common;
using Osom.FluentRestult.Domain.Persistence;

namespace Osom.FluentRestult.Application.Features.Users.Commands.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Result>
    {
        private readonly IUserRepository _userRepository;

        public DeleteUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result> Handle(
            DeleteUserCommand request,
            CancellationToken cancellationToken
        )
        {
            var user = await _userRepository.GetAsync(request.Id);
            if (user is null)
            {
                return Result.Fail(new NotFoundError("User not found"));
            }
            await _userRepository.DeleteAsync(user);
            return Result.Ok();
        }
    }
}
