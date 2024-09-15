using Microsoft.EntityFrameworkCore;

namespace Aurora.Framework.Infrastructure.Outbox;

public static class OutboxMessageExtensions
{
    public static void ApplyOutboxMessageConfiguration(this ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new OutboxMessageConfiguration());
    }
}