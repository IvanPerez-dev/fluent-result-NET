using Microsoft.AspNetCore.Mvc;
using Osom.FluentRestult.Application.Features.Users.Commands.CreateUser;
using Osom.FluentRestult.Application.Features.Users.Commands.DeleteUser;
using Osom.FluentRestult.Application.Features.Users.Queries.GetAllUsers;
using Osom.FluentRestult.Domain.Errors.Users;

namespace Osom.FluentRestult.API.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetALl()
        {
            var result = await Mediator.Send(new GetAllUsersQuery());
            return Map(result).Ok().Build();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserCommand command)
        {
            var result = await Mediator.Send(command);
            return Map(result).Created().ConflictFor<UserAlreadyExistsError>().Build();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delte(int id)
        {
            var result = await Mediator.Send(new DeleteUserCommand(id));
            return Map(result).NoContent().Build();
        }
    }
}
