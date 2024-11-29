namespace Aurora.BizSuite.Items.Infrastructure.Items;

internal class ItemConfiguration : IEntityTypeConfiguration<Item>
{
    public void Configure(EntityTypeBuilder<Item> builder)
    {
        // Keys
        builder
            .HasKey(e => e.Id)
            .HasName("PK_Item");

        // Properties
        builder
            .Property(p => p.Id)
            .HasColumnName("ItemId")
            .HasConversion(id => id.Value, value => new ItemId(value))
            .IsRequired();

        builder
            .Property(p => p.CategoryId)
            .HasConversion(id => id.Value, value => new CategoryId(value))
            .IsRequired();

        builder
            .Property(p => p.BrandId)
            .HasConversion(id => id.Value, value => new BrandId(value))
            .IsRequired();

        builder
            .Property(p => p.Code)
            .IsRequired()
            .HasMaxLength(40);

        builder
            .Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder
            .Property(p => p.Description)
            .IsRequired()
            .HasMaxLength(1000);

        builder
            .Property(p => p.ItemType)
            .HasConversion<string>()
            .HasMaxLength(20);

        builder
            .Property(p => p.AlternativeCode)
            .HasMaxLength(40);

        builder
            .Property(p => p.Notes)
            .HasMaxLength(1000);

        builder
            .Property(p => p.Tags)
            .HasMaxLength(4000);

        builder
            .Property(p => p.Status)
            .IsRequired();

        builder.AddAuditableProperties();

        // Indexes
        builder
            .HasIndex(i => i.Code)
            .IsUnique()
            .HasDatabaseName("UK_Item");

        builder
            .HasIndex(i => i.CategoryId)
            .HasDatabaseName("IX_Item_CategoryId");

        builder
            .HasIndex(i => i.BrandId)
            .HasDatabaseName("IX_Item_BrandId");

        // Relationships
        builder
            .HasOne(e => e.Category)
            .WithMany()
            .HasForeignKey(e => e.CategoryId)
            .IsRequired();
    }
}