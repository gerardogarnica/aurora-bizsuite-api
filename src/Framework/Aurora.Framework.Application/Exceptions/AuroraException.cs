namespace Aurora.Framework.Application;

public class AuroraException(string requestName, BaseError? error, Exception? innerException = default)
    : Exception("Aurora BizSuite exception", innerException)
{
    public string RequestName { get; } = requestName;
    public BaseError? Error { get; } = error;
}