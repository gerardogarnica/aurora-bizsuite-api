using Aurora.Framework.Infrastructure.Serialization;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Aurora.Framework.Infrastructure.Inbox;

public abstract class BaseIntegrationEventConsumer(DbContext dbContext)
{
    public async Task ProcessIntegrationEventMessage(IIntegrationEvent message)
    {
        var inboxMessage = new InboxMessage
        {
            Id = message.Id,
            Type = message.GetType().Name,
            Content = JsonConvert.SerializeObject(message, SerializerSettings.Instance),
            OccurredOnUtc = message.OccurredOnUtc,
            IsProcessed = false
        };

        await dbContext.Set<InboxMessage>().AddRangeAsync(inboxMessage);

        await dbContext.SaveChangesAsync();
    }
}