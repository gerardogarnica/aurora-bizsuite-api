using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;

namespace Aurora.Framework.Infrastructure;

public sealed class PublishDomainEventsInterceptor(
    IServiceScopeFactory serviceScopeFactory)
    : SaveChangesInterceptor
{
    public override async ValueTask<int> SavedChangesAsync(
        SaveChangesCompletedEventData eventData,
        int result,
        CancellationToken cancellationToken = default)
    {
        if (eventData.Context is not null)
            await PublishDomainEventsAsync(eventData.Context);

        return await base.SavedChangesAsync(eventData, result, cancellationToken);
    }

    private async Task PublishDomainEventsAsync(DbContext context)
    {
        var domainEvents = context
            .ChangeTracker
            .Entries<IAggregateRoot>()
            .Select(x => x.Entity)
            .Where(x => x.DomainEvents.Count != 0)
            .SelectMany(x =>
            {
                var domainEvents = x.DomainEvents;
                x.ClearDomainEvents();

                return domainEvents;
            })
            .ToList();

        using IServiceScope serviceScope = serviceScopeFactory.CreateScope();

        IPublisher publisher = serviceScope.ServiceProvider.GetRequiredService<IPublisher>();

        foreach (var domainEvent in domainEvents)
        {
            await publisher.Publish(domainEvent);
        }
    }
}