using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Aurora.Framework.Application;

internal sealed class PerformanceBehavior<TRequest, TResponse>(
    ILogger<PerformanceBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly Stopwatch _timer = new();

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        _timer.Start();

        TResponse response = await next();

        _timer.Stop();

        if (_timer.ElapsedMilliseconds > 500)
        {
            logger.LogWarning("Long-running request: {Name} ({ElapsedMilliseconds} milliseconds) {@Request}",
                typeof(TRequest).Name, _timer.ElapsedMilliseconds, request);
        }

        return response;
    }
}