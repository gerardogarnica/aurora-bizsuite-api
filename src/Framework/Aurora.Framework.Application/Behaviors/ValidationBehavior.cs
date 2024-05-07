using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace Aurora.Framework.Application;

internal sealed class ValidationBehavior<TRequest, TResponse>(
    IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IBaseCommand
    where TResponse : Result
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        ValidationFailure[] failures = await ValidateAsync(request);
        if (failures.Length == 0) return await next();

        throw new ValidationException(failures);
    }

    private async Task<ValidationFailure[]> ValidateAsync(TRequest request)
    {
        if (!validators.Any()) return [];

        var context = new ValidationContext<TRequest>(request);

        var results = await Task.WhenAll(
            validators.Select(v => v.ValidateAsync(request)));

        var failures = results
            .Where(x => !x.IsValid)
            .SelectMany(x => x.Errors)
            .Distinct()
            .ToArray();

        return failures;
    }
}