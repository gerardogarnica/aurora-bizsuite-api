namespace Aurora.BizSuite.Items.Infrastructure.Items;

internal class RelatedItemConfiguration : IEntityTypeConfiguration<RelatedItem>
{
    public void Configure(EntityTypeBuilder<RelatedItem> builder)
    {
        // Keys
        builder
            .HasKey(e => e.Id)
            .HasName("PK_RelatedItem");

        // Properties
        builder
            .Property(p => p.Id)
            .HasColumnName("RelatedItemId")
            .IsRequired()
            .ValueGeneratedNever();

        builder.Property(p => p.ItemId).IsRequired();

        builder
            .Property(p => p.RelatedItemId)
            .HasColumnName("RelatedId")
            .HasConversion(id => id.Value, value => new ItemId(value))
            .IsRequired();

        // Indexes
        builder
            .HasIndex(i => i.ItemId)
            .HasDatabaseName("IX_RelatedItem_ItemId");

        // Foreign keys
        builder
            .HasOne<Item>()
            .WithMany(n => n.RelatedItems)
            .HasForeignKey(f => f.ItemId)
            .IsRequired();
    }
}