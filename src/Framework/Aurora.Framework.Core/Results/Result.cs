namespace Aurora.Framework;

public class Result
{
    protected internal Result(bool isSuccessful, BaseError error)
    {
        if (isSuccessful && error != BaseError.None)
            throw new ArgumentException("Result is successful, but error is not None.", nameof(error));

        if (!isSuccessful && error == BaseError.None)
            throw new ArgumentException("Result is failed, but error is None.", nameof(error));

        IsSuccessful = isSuccessful;
        Error = error;
    }

    public bool IsSuccessful { get; }

    public bool IsFailed => !IsSuccessful;

    public BaseError Error { get; }

    public static Result Ok() => new(true, BaseError.None);

    public static Result<TValue> Ok<TValue>(TValue value) => new(value, true, BaseError.None);

    public static Result Fail(BaseError error) => new(false, error);

    public static Result<TValue> Fail<TValue>(BaseError error) => new(default!, false, error);
}