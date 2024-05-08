using MediatR;
using Microsoft.Extensions.Logging;
using Serilog.Context;
using System.Text;

namespace Aurora.Framework.Application;

internal sealed class LoggingBehavior<TRequest, TResponse>(
    ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : Result
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var appName = LoggingBehavior<TRequest, TResponse>.GetRequestAppName(typeof(TRequest).FullName!);

        using (LogContext.PushProperty("Module", appName))
        {
            logger.LogInformation("Processing request: {Name} {@Request}", typeof(TRequest).Name, request);

            TResponse result = await next();

            if (result.IsSuccessful)
            {
                logger.LogInformation("Request processed successfully: {Name} {@Response}", typeof(TResponse).Name, result);
            }
            else
            {
                using (LogContext.PushProperty("Errors", result.Error, true))
                {
                    logger.LogError("Request processed with errors: {Name} {@Response}", typeof(TResponse).Name, result);
                }
            }

            return result;
        }
    }

    private static string GetRequestAppName(string requestFullName)
    {
        var requestToken = requestFullName.Split('.');

        return new StringBuilder()
            .Append(requestToken[0])
            .Append('.')
            .Append(requestToken[1])
            .Append('.')
            .Append(requestToken[2])
            .ToString();
    }
}