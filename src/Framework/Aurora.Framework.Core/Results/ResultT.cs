namespace Aurora.Framework;

public class Result<TValue> : Result
{
    private readonly TValue _value;

    protected internal Result(TValue value, bool isSuccessful, BaseError error)
        : base(isSuccessful, error)
        => _value = value;

    public TValue Value => IsSuccessful
        ? _value
        : throw new InvalidOperationException("There is no value for failed result.");

    public static implicit operator Result<TValue>(TValue value) => Ok(value);
}