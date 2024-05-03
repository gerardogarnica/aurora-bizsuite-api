using Aurora.Framework.Application;

namespace Aurora.Framework.Infrastructure.Time;

internal class DateTimeService : IDateTimeService
{
    public DateTime UtcNow => DateTime.UtcNow;
}