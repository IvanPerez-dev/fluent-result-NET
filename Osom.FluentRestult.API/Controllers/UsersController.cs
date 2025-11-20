using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Osom.FluentRestult.Application.Features.Users.Commands.CreateUser;
using Osom.FluentRestult.Application.Features.Users.Queries.GetAllUsers;
using Osom.FluentRestult.Domain.Errors;

namespace Osom.FluentRestult.API.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetALl()
        {
            return Ok(await Mediator.Send(new GetAllUsersQuery()));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserCommand command)
        {
            var result = await Mediator.Send(command);

            return Map(result)
                .Created("/api/users/{id}") // explícito: envío la lista tal cual
                .NotFoundFor<UserNotFoundError>()
                .Build();
        }
    }
}
