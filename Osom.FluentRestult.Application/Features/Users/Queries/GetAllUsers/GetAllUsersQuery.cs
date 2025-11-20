using MediatR;

namespace Osom.FluentRestult.Application.Features.Users.Queries.GetAllUsers
{
    public class GetAllUsersQuery : IRequest<List<GetAllUsersResponse>> { }
}
