using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Osom.FluentRestult.API.Extensions;

namespace Osom.FluentRestult.API.Controllers
{
    public class BaseApiController : ControllerBase
    {
        private IMediator? _mediator;
        protected IMediator Mediator =>
            _mediator ??= HttpContext.RequestServices.GetRequiredService<IMediator>();

        protected ResultMapper<T> Map<T>(Result<T> result) => new(result, this);

        protected ResultMapper<object> Map(Result result) => new(result.ToResult<object>(), this);
    }
}
