using Aurora.Framework.Application;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Aurora.Framework.Infrastructure.Inbox;

public abstract class IntegrationEventConsumer<TIntegrationEvent>(
    DbContext dbContext) : IConsumer<TIntegrationEvent>
    where TIntegrationEvent : IntegrationEvent
{
    public async Task Consume(ConsumeContext<TIntegrationEvent> context)
    {
        TIntegrationEvent integrationEvent = context.Message;

        var inboxMessage = new InboxMessage
        {
            Id = integrationEvent.Id,
            Type = integrationEvent.GetType().Name,
            Content = JsonConvert.SerializeObject(integrationEvent, Serialization.SerializerSettings.Instance),
            OccurredOnUtc = integrationEvent.OccurredOnUtc,
            IsProcessed = false
        };

        await InsertInboxMessageAsync(inboxMessage);
    }

    public async Task InsertInboxMessageAsync(InboxMessage message)
    {
        await dbContext.Set<InboxMessage>().AddRangeAsync(message);
    }
}