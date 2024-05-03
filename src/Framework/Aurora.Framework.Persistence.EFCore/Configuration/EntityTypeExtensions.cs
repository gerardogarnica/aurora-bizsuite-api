using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aurora.Framework.Persistence.EFCore;

public static class EntityTypeExtensions
{
    public static void AddAuditableProperties<T>(
        this EntityTypeBuilder<T> builder) where T : class, IAuditableEntity
    {
        builder.Property(p => p.CreatedBy).IsRequired().HasMaxLength(100);
        builder.Property(p => p.CreatedAt).IsRequired();
        builder.Property(p => p.UpdatedBy).HasMaxLength(100);
        builder.Property(p => p.UpdatedAt);
    }

    public static void AddSoftDeletableProperties<T>(
        this EntityTypeBuilder<T> builder) where T : class, ISoftDeletable
    {
        builder.Property(p => p.IsDeleted).IsRequired();
        builder.Property(p => p.DeletedBy).HasMaxLength(100);
        builder.Property(p => p.DeletedAt);
    }
}