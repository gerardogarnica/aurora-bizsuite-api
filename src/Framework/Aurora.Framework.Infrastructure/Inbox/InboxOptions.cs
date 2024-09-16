namespace Aurora.Framework.Infrastructure.Inbox;

public sealed class InboxOptions
{
    public int IntervalInSeconds { get; init; }
    public int BatchSize { get; init; }
}