using Aurora.Framework.Application;
using MassTransit;

namespace Aurora.Framework.Infrastructure.EventBus;

internal sealed class EventBus(IBus bus) : IEventBus
{
    public async Task PublishAsync<TIntegrationEvent>(
        TIntegrationEvent integrationEvent,
        CancellationToken cancellationToken = default)
        where TIntegrationEvent : IIntegrationEvent
    {
        await bus.Publish(integrationEvent, cancellationToken);
    }
}