using Aurora.Framework.Application.Time;

namespace Aurora.Framework.Infrastructure.Time;

internal class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}