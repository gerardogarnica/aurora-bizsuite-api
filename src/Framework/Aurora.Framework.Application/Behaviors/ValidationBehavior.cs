using FluentValidation;
using MediatR;

namespace Aurora.Framework.Application;

public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : Result
{
    private readonly IEnumerable<IValidator<TRequest>> _validators = validators;

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (!_validators.Any()) return await next();

        var results = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(request)));

        var failures = results
            .Where(x => x.Errors.Count != 0)
            .SelectMany(x => x.Errors)
            .Distinct()
            .ToList();

        if (failures.Count == 0) return await next();

        throw new ValidationException(failures);
    }
}