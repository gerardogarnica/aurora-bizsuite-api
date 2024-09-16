using Microsoft.EntityFrameworkCore;

namespace Aurora.Framework.Infrastructure.Inbox;

public static class InboxMessageExtensions
{
    public static void ApplyInboxMessageConfiguration(this ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new InboxMessageConfiguration());
    }
}