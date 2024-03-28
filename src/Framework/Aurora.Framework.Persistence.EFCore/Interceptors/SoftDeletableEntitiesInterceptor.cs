using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Aurora.Framework.Persistence.EFCore;

public sealed class SoftDeletableEntitiesInterceptor : SaveChangesInterceptor
{
    public SoftDeletableEntitiesInterceptor()
    {
        // TODO: implement HttpContextAccessor to get the current user
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        if (eventData.Context is not null)
            UpdateSoftDeletableEntities(eventData.Context);

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private static void UpdateSoftDeletableEntities(DbContext context)
    {
        var now = DateTime.UtcNow;
        var user = "System";
        var softDeletableEntities = context
            .ChangeTracker
            .Entries<ISoftDeletable>()
            .Where(e => e.State == EntityState.Deleted);

        foreach (var entry in softDeletableEntities)
        {
            entry.Entity.IsDeleted = true;
            entry.Entity.DeletedBy = user;
            entry.Entity.DeletedAt = now;
            entry.State = EntityState.Modified;
        }
    }
}