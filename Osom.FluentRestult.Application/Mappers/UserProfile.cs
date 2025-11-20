using AutoMapper;
using Osom.FluentRestult.Application.Features.Users.Commands.CreateUser;
using Osom.FluentRestult.Application.Features.Users.Queries.GetAllUsers;
using Osom.FluentRestult.Domain.Entities;

namespace Osom.FluentRestult.Application.Mappers
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, CreateUserResponse>();
            CreateMap<User, GetAllUsersResponse>();
        }
    }
}
