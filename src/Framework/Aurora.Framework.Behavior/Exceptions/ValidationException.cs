using FluentValidation.Results;

namespace Aurora.Framework.Behavior;

public class ValidationException : Exception
{
    public IDictionary<string, string[]> Errors { get; }

    public ValidationException()
        : base("One or more validation failures have occurred.")
    {
        Errors = new Dictionary<string, string[]>();
    }

    public ValidationException(IEnumerable<ValidationFailure> errors)
        : this()
    {
        Errors = errors
            .GroupBy(e => e.ErrorCode, e => e.ErrorMessage)
            .ToDictionary(group => group.Key, group => group.ToArray());
    }
}