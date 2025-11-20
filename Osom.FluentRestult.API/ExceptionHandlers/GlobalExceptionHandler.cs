using Microsoft.AspNetCore.Diagnostics;
using Osom.FluentRestult.Application.Dtos.Common;
using Osom.FluentRestult.Application.Exceptions;
using Osom.FluentRestult.Domain.Interfaces;

namespace Osom.FluentRestult.API.ExceptionHandlers
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(
            IWebHostEnvironment env,
            ILogger<GlobalExceptionHandler> logger
        )
        {
            _env = env;
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken
        )
        {
            _logger.LogError(exception, "Unhandled exception: {Message}", exception.Message);

            var problemDetails = CreateProblemDetails(exception);
            httpContext.Response.StatusCode = problemDetails.Status.Value;

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
            return true;
        }

        private CustomProblemDetails CreateProblemDetails(Exception exception)
        {
            return exception switch
            {
                ICustomException custom => new CustomProblemDetails
                {
                    Title = custom.Title,
                    Status = custom.StatusCode,
                    Detail = custom.Detail,
                    Type = custom.Type,
                },
                ValidationException validation => new CustomProblemDetails
                {
                    Title = "Validation error",
                    Status = StatusCodes.Status400BadRequest,
                    Detail = validation.Message,
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                    Errors = validation.Failures,
                },
                _ => new CustomProblemDetails
                {
                    Title = "Internal Server Error",
                    Status = StatusCodes.Status500InternalServerError,
                    Detail = _env.IsDevelopment() ? exception.Message : "An error occurred",
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1",
                },
            };
        }
    }
}
