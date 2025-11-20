using AutoMapper;
using FluentResults;
using MediatR;
using Osom.FluentRestult.Domain.Entities;
using Osom.FluentRestult.Domain.Errors;
using Osom.FluentRestult.Domain.Persistence;

namespace Osom.FluentRestult.Application.Features.Users.Commands.CreateUser
{
    internal class CreateUserCommandHandler
        : IRequestHandler<CreateUserCommand, Result<CreateUserResponse>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public CreateUserCommandHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<Result<CreateUserResponse>> Handle(
            CreateUserCommand request,
            CancellationToken cancellationToken
        )
        {
            var userCreated = User.Create(
                request.FirstName,
                request.LastName,
                request.Email,
                request.Password
            );

            //return Result.Fail<CreateUserResponse>(new UserNotFoundError(1));

            if (userCreated.IsFailed)
            {
                return Result.Fail<CreateUserResponse>(userCreated.Errors);
            }

            await _userRepository.AddAsync(userCreated.Value);
            var userResponse = _mapper.Map<CreateUserResponse>(userCreated.Value);

            return Result.Ok(userResponse);
        }
    }
}
