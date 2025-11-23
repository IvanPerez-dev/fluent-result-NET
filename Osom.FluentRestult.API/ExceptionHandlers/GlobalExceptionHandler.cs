using Microsoft.AspNetCore.Diagnostics;
using Osom.FluentRestult.API.Configurations;
using Osom.FluentRestult.Application.Dtos.Common;

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
            //httpContext.Response.StatusCode = problemDetails.Status.Value;

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
            return true;
        }

        private CustomProblemDetails CreateProblemDetails(Exception exception)
        {
            var errorTypeInfo = ErrorConfiguration.GetErrorInfo(500);
            return new CustomProblemDetails
            {
                Title = errorTypeInfo.Title,
                Status = StatusCodes.Status500InternalServerError,
                Detail = _env.IsDevelopment() ? exception.Message : "An error occurred",
                Type = errorTypeInfo.Type,
            };
        }
    }
}
