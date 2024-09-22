namespace Aurora.Framework;

public interface IIntegrationEvent
{
    Guid Id { get; }
    DateTime OccurredOnUtc { get; }
}