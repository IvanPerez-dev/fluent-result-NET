using AutoMapper;
using FluentResults;
using MediatR;
using Osom.FluentRestult.Domain.Entities;
using Osom.FluentRestult.Domain.Errors.Users;
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
            var userExists = await _userRepository.GetAsync(request.Email);
            if (userExists is not null)
            {
                return Result.Fail<CreateUserResponse>(new UserAlreadyExistsError(request.Email));
            }

            var userCreated = User.Create(
                request.FirstName,
                request.LastName,
                request.Email,
                request.Password
            );

            //if (userCreated.IsFailed)
            //{
            //    return Result.Fail<CreateUserResponse>(userCreated.Errors);
            //}

            if (userCreated.IsFailed)
                return userCreated.ToResult<CreateUserResponse>();

            await _userRepository.AddAsync(userCreated.Value);
            var userResponse = _mapper.Map<CreateUserResponse>(userCreated.Value);

            return Result.Ok(userResponse);
        }
    }
}
