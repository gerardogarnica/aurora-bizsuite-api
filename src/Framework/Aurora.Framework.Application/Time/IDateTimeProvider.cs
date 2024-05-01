namespace Aurora.Framework.Application.Time;

public interface IDateTimeProvider
{
    public DateTime UtcNow { get; }
}