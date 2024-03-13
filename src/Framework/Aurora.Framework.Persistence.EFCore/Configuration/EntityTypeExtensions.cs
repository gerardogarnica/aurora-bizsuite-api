using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aurora.Framework.Persistence.EFCore;

public static class EntityTypeExtensions
{
    public static void AddAuditableProperties<T, TId>(
        this EntityTypeBuilder<T> builder) where T : AuditableEntity<TId>
    {
        builder.Property(p => p.CreatedBy).IsRequired().HasMaxLength(100);
        builder.Property(p => p.CreatedAt).IsRequired();
        builder.Property(p => p.UpdatedBy).HasMaxLength(100);
        builder.Property(p => p.UpdatedAt);
    }
}