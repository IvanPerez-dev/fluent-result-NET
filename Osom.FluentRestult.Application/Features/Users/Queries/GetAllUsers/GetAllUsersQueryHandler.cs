using AutoMapper;
using Osom.FluentRestult.Domain.Persistence;
using MediatR;

namespace Osom.FluentRestult.Application.Features.Users.Queries.GetAllUsers
{
    internal class GetAllUsersQueryHandler
        : IRequestHandler<GetAllUsersQuery, List<GetAllUsersResponse>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetAllUsersQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<List<GetAllUsersResponse>> Handle(
            GetAllUsersQuery request,
            CancellationToken cancellationToken
        )
        {
            var users = await _userRepository.GetAllAsync();
            return _mapper.Map<List<GetAllUsersResponse>>(users);
        }
    }
}
