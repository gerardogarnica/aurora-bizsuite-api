namespace Aurora.BizSuite.Items.Infrastructure.Brands;

internal class BrandConfiguration : IEntityTypeConfiguration<Brand>
{
    public void Configure(EntityTypeBuilder<Brand> builder)
    {
        // Keys
        builder
            .HasKey(e => e.Id)
            .HasName("PK_Brand");

        // Properties
        builder
            .Property(p => p.Id)
            .HasColumnName("BrandId")
            .HasConversion(id => id.Value, value => new BrandId(value))
            .IsRequired();

        builder
            .Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder
            .Property(p => p.LogoUri)
            .HasMaxLength(1000);

        builder
            .Property(p => p.Notes)
            .HasMaxLength(1000);

        builder.AddAuditableProperties();
        builder.AddSoftDeletableProperties();

        // Relationships
        builder
            .HasMany(e => e.Items)
            .WithOne(e => e.Brand)
            .HasForeignKey(e => e.BrandId)
            .HasPrincipalKey(e => e.Id)
            .HasConstraintName("FK_Item_Brand")
            .IsRequired();
    }
}