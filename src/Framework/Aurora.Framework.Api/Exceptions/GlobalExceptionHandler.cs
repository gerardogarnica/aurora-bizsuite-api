using Aurora.Framework.Application;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Aurora.Framework.Api;

public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        logger.LogError(exception, "Exception ocurred: {Message}", exception.Message);

        string? json;

        if (exception is ValidationException validationException)
        {
            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            json = ConvertToValidationProblemDetails(validationException);
        }
        else
        {
            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            json = ConvertToProblemDetails();
        }

        httpContext.Response.ContentType = "application/json";
        await httpContext.Response.WriteAsync(json, cancellationToken);

        return true;
    }

    private static string ConvertToProblemDetails()
    {
        var error = new ProblemDetails
        {
            Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1",
            Title = "Internal Server Error",
            Status = StatusCodes.Status500InternalServerError,
            Detail = "An error occurred while processing your request."
        };

        return JsonSerializer.Serialize(error);
    }

    private static string ConvertToValidationProblemDetails(ValidationException exception)
    {
        var error = new ValidationProblemDetails(exception.Errors)
        {
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
            Title = exception.Message,
            Status = StatusCodes.Status400BadRequest,
            Detail = "See the errors property for details."
        };

        return JsonSerializer.Serialize(error);
    }
}