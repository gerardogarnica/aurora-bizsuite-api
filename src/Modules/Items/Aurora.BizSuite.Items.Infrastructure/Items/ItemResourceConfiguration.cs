namespace Aurora.BizSuite.Items.Infrastructure.Items;

internal class ItemResourceConfiguration : IEntityTypeConfiguration<ItemResource>
{
    public void Configure(EntityTypeBuilder<ItemResource> builder)
    {
        // Keys
        builder
            .HasKey(e => e.Id)
            .HasName("PK_ItemResource");

        // Properties
        builder
            .Property(p => p.Id)
            .HasColumnName("ItemResourceId")
            .IsRequired()
            .ValueGeneratedNever();

        builder.Property(p => p.ItemId).IsRequired();
        builder.Property(p => p.Type).IsRequired().HasMaxLength(40);
        builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
        builder.Property(p => p.Uri).IsRequired().HasMaxLength(1000);
        builder.Property(p => p.Order).IsRequired();

        // Indexes
        builder
            .HasIndex(i => i.ItemId)
            .HasDatabaseName("IX_ItemResource_ItemId");

        // Foreign keys
        builder
            .HasOne<Item>()
            .WithMany(n => n.Resources)
            .HasForeignKey(f => f.ItemId)
            .IsRequired()
            .HasConstraintName("FK_ItemResource_Item");
    }
}