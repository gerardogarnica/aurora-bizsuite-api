using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Aurora.Framework.Application;

public class PerformanceBehavior<TRequest, TResponse>(ILogger<TRequest> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly Stopwatch _timer = new();
    private readonly ILogger<TRequest> _logger = logger;

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        _timer.Start();

        var response = await next();

        _timer.Stop();

        if (_timer.ElapsedMilliseconds > 500)
        {
            var requestName = typeof(TRequest).Name;

            _logger.LogWarning("Aurora Soft long running request: {Name} ({ElapsedMilliseconds} milliseconds) {@Request}",
                requestName, _timer.ElapsedMilliseconds, request);
        }

        return response;
    }
}