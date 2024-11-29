namespace Aurora.BizSuite.Items.Infrastructure.Items;

internal class ItemUnitConfiguration : IEntityTypeConfiguration<ItemUnit>
{
    public void Configure(EntityTypeBuilder<ItemUnit> builder)
    {
        // Keys
        builder
            .HasKey(e => e.Id)
            .HasName("PK_ItemUnit");

        // Properties
        builder
            .Property(p => p.Id)
            .HasColumnName("ItemUnitId")
            .IsRequired()
            .ValueGeneratedNever();

        builder
            .Property(p => p.UnitId)
            .HasColumnName("UnitId")
            .HasConversion(id => id.Value, value => new UnitOfMeasurementId(value))
            .IsRequired();

        builder.Property(p => p.ItemId).IsRequired();
        builder.Property(p => p.IsPrimary).IsRequired();
        builder.Property(p => p.AvailableForReceipt).IsRequired();
        builder.Property(p => p.AvailableForDispatch).IsRequired();
        builder.Property(p => p.UseDecimals).IsRequired();
        builder.Property(p => p.IsEditable).IsRequired();

        // Indexes
        builder
            .HasIndex(i => new { i.ItemId, i.UnitId })
            .IsUnique()
            .HasDatabaseName("UK_ItemUnit");

        builder
            .HasIndex(i => i.ItemId)
            .HasDatabaseName("IX_ItemUnit_ItemId");

        builder
            .HasIndex(i => i.UnitId)
            .HasDatabaseName("IX_ItemUnit_UnitId");

        // Foreign keys
        builder
            .HasOne<Item>()
            .WithMany(n => n.Units)
            .HasForeignKey(f => f.ItemId)
            .IsRequired();
    }
}