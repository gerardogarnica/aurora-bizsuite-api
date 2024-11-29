namespace Aurora.BizSuite.Items.Infrastructure.Items;

internal class ItemCategoryConfiguration : IEntityTypeConfiguration<ItemCategory>
{
    public void Configure(EntityTypeBuilder<ItemCategory> builder)
    {
        // Keys
        builder
            .HasKey(i => new { i.ItemId, i.CategoryId })
            .HasName("PK_ItemCategory");

        // Properties
        builder.Property(p => p.ItemId).IsRequired();
        builder
            .Property(p => p.CategoryId)
            .HasConversion(id => id.Value, value => new CategoryId(value))
            .IsRequired();

        // Indexes
        builder
            .HasIndex(i => i.ItemId)
            .HasDatabaseName("IX_ItemCategory_ItemId");

        builder
            .HasIndex(i => i.CategoryId)
            .HasDatabaseName("IX_ItemCategory_CategoryId");

        // Foreign keys
        builder
            .HasOne<Item>()
            .WithMany(n => n.Categories)
            .HasForeignKey(f => f.ItemId)
            .IsRequired()
            .OnDelete(DeleteBehavior.ClientCascade);
    }
}