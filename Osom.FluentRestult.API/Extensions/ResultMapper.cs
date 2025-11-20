using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Osom.FluentRestult.API.Configurations;
using Osom.FluentRestult.Application.Dtos.Common;
using Osom.FluentRestult.Domain.Errors.Common;

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
            ConfigureDefaultErrorMapper();
        }

        public ResultMapper<T> OkEmpty()
        {
            _successFactory = () => _controller.Ok();
            return this;
        }

        /// <summary>
        /// Configura 200 OK devolviendo result.Value tal cual (sin modificaciones).
        /// Úsalo cuando el DTO ya viene listo de Application.
        /// </summary>
        public ResultMapper<T> Ok() // si no pasas nada → usa result.Value
        {
            _successFactory = () =>
            {
                var value = _result.Value;
                if (value is null)
                    return _controller.Ok();
                return _controller.Ok(value);
            };
            return this;
        }

        /// <summary>
        /// Configura 201 Created devolviendo result.Value y Location header automática basada en Id.
        /// Tambien puedes pasar un uri personalizado.
        /// </summary>
        public ResultMapper<T> Created(string uri = null)
        {
            var requestPath = _controller.Url.ActionContext.HttpContext.Request.Path;
            var idProperty = _result.ValueOrDefault.GetType().GetProperty("Id");
            var idValue = idProperty?.GetValue(_result.ValueOrDefault);

            _successFactory = () =>
                _controller.Created(uri ?? $"{requestPath}/{idValue}", _result.Value);
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

        private void ConfigureDefaultErrorMapper()
        {
            AddErrorMapper<NotFoundError>(err =>
                _controller.NotFound(Problem(err, StatusCodes.Status404NotFound))
            );
            AddErrorMapper<ConflictError>(err =>
                _controller.Conflict(Problem(err, StatusCodes.Status409Conflict))
            );

            AddErrorMapper<ValidationError>(err =>
                _controller.BadRequest(Problem(err, StatusCodes.Status400BadRequest))
            );
            AddErrorMapper<UnauthorizedError>(err =>
                _controller.Unauthorized(Problem(err, StatusCodes.Status401Unauthorized))
            );
            AddErrorMapper<ForbiddenError>(err =>
                _controller.StatusCode(
                    StatusCodes.Status403Forbidden,
                    Problem(err, StatusCodes.Status403Forbidden)
                )
            );
            //AddErrorMapper<BusinessRuleError>(err =>
            //    _controller.UnprocessableEntity(
            //        Problem(err, StatusCodes.Status422UnprocessableEntity)
            //    )
            //);
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
            var title = status switch
            {
                400 => "Invalid request",
                401 => "Unauthorized",
                403 => "Forbidden",
                404 => "Resource not found",
                409 => "Conflict",
                422 => "Business rule violation",
                _ => "An error occurred",
            };

            string typeUrl = ErrorTypes.Documentation.GetValueOrDefault(
                status,
                "https://tools.ietf.org/html/rfc2616#section-10"
            );
            var code = error.Metadata.TryGetValue("ErrorCode", out var c)
                ? c?.ToString() ?? "UNKNOWN_ERROR"
                : "UNKNOWN_ERROR";

            var errors = error.Metadata["Errors"] as Dictionary<string, List<string>>;
            return new CustomProblemDetails(
                title: title,
                detail: error.Message,
                type: typeUrl,
                status: status,
                instance: _controller.HttpContext.Request.Path
            )
            {
                Code = code,
                Errors = errors,
            };
        }
    }
}
