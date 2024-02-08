using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Aurora.Framework.Persistence.EFCore;

public sealed class AuditableEntitiesInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        if (eventData.Context is not null)
            UpdateAuditableEntities(eventData.Context);

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private static void UpdateAuditableEntities(DbContext context)
    {
        var now = DateTime.UtcNow;
        var user = "System";
        var auditableEntities = context
            .ChangeTracker
            .Entries<IAuditableEntity>();

        foreach (var entry in auditableEntities)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.SetCreated(user, now);
            }

            if (entry.State == EntityState.Modified)
            {
                entry.Entity.SetUpdated(user, now);
            }
        }
    }
}