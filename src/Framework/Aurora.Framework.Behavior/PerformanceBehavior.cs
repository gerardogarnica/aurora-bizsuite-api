using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Aurora.Framework.Behavior;

public class PerformanceBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull, IRequest<TResponse>
    where TResponse : class
{
    private readonly Stopwatch _timer;
    private readonly ILogger<TRequest> _logger;

    public PerformanceBehavior(
        ILogger<TRequest> logger)
    {
        _timer = new Stopwatch();
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        _timer.Start();

        var response = await next();

        _timer.Stop();

        if (_timer.ElapsedMilliseconds > 500)
        {
            var requestName = typeof(TRequest).Name;

            _logger.LogWarning("Aurora Long Running Request: {Name} ({ElapsedMilliseconds} milliseconds) {@Request}",
                requestName, _timer.ElapsedMilliseconds, request);
        }

        return response;
    }
}