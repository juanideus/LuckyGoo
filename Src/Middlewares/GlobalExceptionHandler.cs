using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LUCKYGOO.Src.Exceptions;

namespace Src.Middleware
{
    public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger,IHostEnvironment env) : IExceptionHandler
    {
        public async ValueTask<bool>TryHandleAsync(HttpContext context, Exception exception,CancellationToken cancellationToken)
        {
            var (statusCode,title) = exception switch
            {
                AppException appEx => (appEx.StatusCode, appEx.Message),
                _ => (StatusCodes.Status500InternalServerError, "Error Interno del servidor.")

            };
            if (statusCode == StatusCodes.Status500InternalServerError)
            {
                logger.LogError(exception, "Ocurrió un error inesperado.");                
            }
            else
            {
                logger.LogWarning("Error controlado ({StatusCode}): {Message}", statusCode, exception.Message);
            }
            var problemDetails = new ProblemDetails
            {
                Status = statusCode,
                Title = title,
                Type = $"https://httpstatuses.com/{statusCode}",
                Instance = context.Request.Path,
                Detail = env.IsDevelopment() ? exception.ToString() : null
            };
            problemDetails.Extensions["traceId"] = context.TraceIdentifier;
            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = statusCode;
            await context.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
            return true;

        }
    }
}