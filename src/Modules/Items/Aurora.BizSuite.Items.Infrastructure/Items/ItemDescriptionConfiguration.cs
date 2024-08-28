namespace Aurora.BizSuite.Items.Infrastructure.Items;

internal class ItemDescriptionConfiguration : IEntityTypeConfiguration<ItemDescription>
{
    public void Configure(EntityTypeBuilder<ItemDescription> builder)
    {
        // Keys
        builder
            .HasKey(e => e.Id)
            .HasName("PK_ItemDescription");

        // Properties
        builder
            .Property(p => p.Id)
            .HasColumnName("ItemDescriptionId")
            .IsRequired()
            .ValueGeneratedNever();

        builder.Property(p => p.ItemId).IsRequired();
        builder.Property(p => p.Type).IsRequired().HasMaxLength(40);
        builder.Property(p => p.Description).IsRequired().HasMaxLength(4000);

        // Indexes
        builder
            .HasIndex(i => new { i.ItemId, i.Type })
            .IsUnique()
            .HasDatabaseName("UK_ItemDescription");

        builder
            .HasIndex(i => i.ItemId)
            .HasDatabaseName("IX_ItemDescription_ItemId");

        // Foreign keys
        builder
            .HasOne<Item>()
            .WithMany(n => n.Descriptions)
            .HasForeignKey(f => f.ItemId)
            .IsRequired()
            .HasConstraintName("FK_ItemDescription_Item");
    }
}