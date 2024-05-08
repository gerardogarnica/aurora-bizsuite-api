using Aurora.Framework.Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Aurora.Framework.Infrastructure;

public sealed class SoftDeletableEntitiesInterceptor(
    IDateTimeService dateTimeProvider)
    : SaveChangesInterceptor
{
    // TODO: implement HttpContextAccessor to get the current user

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        if (eventData.Context is not null)
            UpdateSoftDeletableEntities(eventData.Context);

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void UpdateSoftDeletableEntities(DbContext context)
    {
        var user = "System";
        var softDeletableEntities = context
            .ChangeTracker
            .Entries<ISoftDeletable>()
            .Where(e => e.State == EntityState.Deleted);

        foreach (var entry in softDeletableEntities)
        {
            entry.State = EntityState.Modified;

            entry.Property(nameof(ISoftDeletable.IsDeleted)).CurrentValue = true;
            entry.Property(nameof(ISoftDeletable.DeletedBy)).CurrentValue = user;
            entry.Property(nameof(ISoftDeletable.DeletedAt)).CurrentValue = dateTimeProvider.UtcNow;
        }
    }
}