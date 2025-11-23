using AutoMapper;
using FluentResults;
using MediatR;
using Osom.FluentRestult.Domain.Persistence;

namespace Osom.FluentRestult.Application.Features.Users.Queries.GetAllUsers
{
    internal class GetAllUsersQueryHandler
        : IRequestHandler<GetAllUsersQuery, Result<List<GetAllUsersResponse>>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetAllUsersQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<Result<List<GetAllUsersResponse>>> Handle(
            GetAllUsersQuery request,
            CancellationToken cancellationToken
        )
        {
            var users = await _userRepository.GetAllAsync();
            return _mapper.Map<List<GetAllUsersResponse>>(users);
        }
    }
}
