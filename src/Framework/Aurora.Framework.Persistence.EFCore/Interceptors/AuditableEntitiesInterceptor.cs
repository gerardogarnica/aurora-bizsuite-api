using Aurora.Framework.Application.Time;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Aurora.Framework.Persistence.EFCore;

public sealed class AuditableEntitiesInterceptor(
    IDateTimeProvider dateTimeProvider) : SaveChangesInterceptor
{
    // TODO: implement HttpContextAccessor to get the current user

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        if (eventData.Context is not null)
            UpdateAuditableEntities(eventData.Context);

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void UpdateAuditableEntities(DbContext context)
    {
        var user = "System";
        var auditableEntities = context
            .ChangeTracker
            .Entries<IAuditableEntity>();

        foreach (var entry in auditableEntities)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Property(nameof(IAuditableEntity.CreatedBy)).CurrentValue = user;
                entry.Property(nameof(IAuditableEntity.CreatedAt)).CurrentValue = dateTimeProvider.UtcNow;
            }

            if (entry.State == EntityState.Modified)
            {
                entry.Property(nameof(IAuditableEntity.UpdatedBy)).CurrentValue = user;
                entry.Property(nameof(IAuditableEntity.UpdatedAt)).CurrentValue = dateTimeProvider.UtcNow;
            }
        }
    }
}