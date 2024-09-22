namespace Aurora.Framework.Application;

public interface IIntegrationEventHandler<T> where T : IIntegrationEvent
{
    Task Handle(T integrationEvent, CancellationToken cancellationToken = default);
}