namespace Aurora.Framework;

public sealed record BaseError(string Code, string Message)
{
    public static readonly BaseError None = new(string.Empty, string.Empty);
    public static readonly BaseError NullValue= new("Error.NullValue", "The result value is null.");

    public static implicit operator Result(BaseError error) => Result.Fail(error);
}