using Aurora.Framework.Infrastructure.Outbox;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Newtonsoft.Json;

namespace Aurora.Framework.Infrastructure.Interceptors;

public sealed class InsertOutboxMessagesInterceptor : SaveChangesInterceptor
{
    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        if (eventData.Context is not null)
        {
            InsertOutboxMessages(eventData.Context);
        }

        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private static void InsertOutboxMessages(DbContext context)
    {
        var outboxMessages = context
            .ChangeTracker
            .Entries<IAggregateRoot>()
            .Select(x => x.Entity)
            .Where(x => x.DomainEvents.Count > 0)
            .SelectMany(x =>
            {
                var domainEvents = x.DomainEvents.ToList();
                x.ClearDomainEvents();

                return domainEvents;
            })
            .Select(domainEvent => new OutboxMessage
            {
                Id = domainEvent.Id,
                Type = domainEvent.GetType().Name,
                Content = JsonConvert.SerializeObject(domainEvent, Serialization.SerializerSettings.Instance),
                OccurredOnUtc = domainEvent.OccurredOnUtc,
                IsProcessed = false
            })
            .ToList();

        context.Set<OutboxMessage>().AddRange(outboxMessages);
    }
}