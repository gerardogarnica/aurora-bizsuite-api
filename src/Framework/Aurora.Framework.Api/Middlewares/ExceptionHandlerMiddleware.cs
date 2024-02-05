using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Aurora.Framework.Api;

public class ExceptionHandlerMiddleware : IMiddleware
{
    private readonly ILogger<ExceptionHandlerMiddleware> _logger;

    public ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger) => _logger = logger;

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            ProblemDetails problemDetails = new()
            {
                Type = "Server Error",
                Title = "An error occurred",
                Status = StatusCodes.Status500InternalServerError,
                Detail = e.Message
            };

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";

            var json = JsonSerializer.Serialize(problemDetails);

            await context.Response.WriteAsync(json);
        }
    }
}