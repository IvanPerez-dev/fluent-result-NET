using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Osom.FluentRestult.Application.Dtos.Common;

namespace Osom.FluentRestult.API.Extensions
{
    public class ResultMapper<T>
    {
        private readonly Result<T> _result;
        private readonly ControllerBase _controller;
        private readonly Dictionary<Type, Func<Error, IActionResult>> _errorMappers = new();
        private Func<IActionResult>? _successFactory;

        internal ResultMapper(Result<T> result, ControllerBase controller)
        {
            _result = result;
            _controller = controller;
        }

        public ResultMapper<T> OkEmpty()
        {
            _successFactory = () => _controller.Ok();
            return this;
        }

        public ResultMapper<T> Ok() // si no pasas nada → usa result.Value
        {
            _successFactory = () => _controller.Ok(_result.Value);
            return this;
        }

        public ResultMapper<T> Created(string uri)
        {
            _successFactory = () => _controller.Created(uri, _result.Value);
            return this;
        }

        public ResultMapper<T> CreatedAtAction(string actionName, object? routeValues = null)
        {
            _successFactory = () =>
                _controller.CreatedAtAction(actionName, routeValues, _result.Value);
            return this;
        }

        public ResultMapper<T> NoContent()
        {
            _successFactory = () => _controller.NoContent();
            return this;
        }

        public ResultMapper<T> Accepted(string? uri = null, T? value = default)
        {
            var body = value ?? _result.ValueOrDefault;
            _successFactory = uri is null
                ? () => _controller.Accepted(body)
                : () => _controller.Accepted(uri, body);
            return this;
        }

        // Mapeo de errores (igual que antes)
        public ResultMapper<T> ConflictFor<TErr>()
            where TErr : Error =>
            AddErrorMapper<TErr>(err => _controller.Conflict(Problem(err, 409)));

        public ResultMapper<T> NotFoundFor<TErr>()
            where TErr : Error =>
            AddErrorMapper<TErr>(err => _controller.NotFound(Problem(err, 404)));

        public ResultMapper<T> BadRequestFor<TErr>()
            where TErr : Error =>
            AddErrorMapper<TErr>(err => _controller.BadRequest(Problem(err, 400)));

        public ResultMapper<T> UnprocessableFor<TErr>()
            where TErr : Error =>
            AddErrorMapper<TErr>(err => _controller.UnprocessableEntity(Problem(err, 422)));

        private ResultMapper<T> AddErrorMapper<TErr>(Func<Error, IActionResult> mapper)
            where TErr : Error
        {
            _errorMappers[typeof(TErr)] = mapper;
            return this;
        }

        public IActionResult Build()
        {
            if (_result.IsSuccess)
            {
                if (_successFactory is null)
                    throw new InvalidOperationException(
                        "Debes configurar una respuesta de éxito: .Ok(), .Created(), .NoContent(), etc."
                    );
                return _successFactory();
            }

            var error = _result.Errors[0] as Error;
            if (_errorMappers.TryGetValue(error.GetType(), out var mapper))
                return mapper(error);

            // Fallback genérico
            return _controller.UnprocessableEntity(Problem(error, 422));
        }

        private CustomProblemDetails Problem(Error error, int status)
        {
            return new CustomProblemDetails(
                title: "Error de negocio",
                detail: error.Message,
                type: $"https://api.tusistema.com/errors/{error.GetType().Name}",
                status: status,
                instance: _controller.HttpContext.Request.Path
            )
            {
                Code = error.GetType().Name,
                //Details = error.Metadata.Count > 0 ? error.Metadata : null,
            };
        }
    }
}
