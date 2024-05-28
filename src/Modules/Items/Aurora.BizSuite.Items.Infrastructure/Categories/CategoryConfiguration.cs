using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Aurora.BizSuite.Items.Infrastructure.Categories;

internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        // Keys
        builder
            .HasKey(e => e.Id)
            .HasName("PK_Category");

        // Properties
        builder
            .Property(p => p.Id)
            .HasColumnName("CategoryId")
            .HasConversion(id => id.Value, value => new CategoryId(value))
            .IsRequired();

        builder
            .Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder
            .Property(p => p.ParentId)
            .HasConversion(
                id => id != null ? id.Value : (Guid?)null,
                value => value.HasValue ? new CategoryId((Guid)value) : null)
            .IsRequired(false);

        builder
            .Property(p => p.Notes)
            .HasMaxLength(1000);

        builder.AddAuditableProperties();

        // Indexes
        builder
            .HasIndex(i => i.ParentId)
            .HasDatabaseName("IX_Category_ParentId");

        // Relationships
        builder
            .HasOne<Category>()
            .WithMany(n => n.Childs)
            .HasForeignKey(f => f.ParentId)
            .IsRequired(false)
            .HasConstraintName("FK_Category_Parent");
    }
}