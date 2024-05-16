namespace Aurora.BizSuite.Items.Infrastructure.Units;

internal class UnitOfMeasurementConfiguration : IEntityTypeConfiguration<UnitOfMeasurement>
{
    public void Configure(EntityTypeBuilder<UnitOfMeasurement> builder)
    {
        // Keys
        builder
            .HasKey(e => e.Id)
            .HasName("PK_UnitOfMeasurement");

        // Properties
        builder
            .Property(p => p.Id)
            .HasColumnName("UnitId")
            .HasConversion(id => id.Value, value => new UnitOfMeasurementId(value))
            .IsRequired();

        builder
            .Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder
            .Property(p => p.Acronym)
            .IsRequired()
            .HasMaxLength(10);

        builder
            .Property(p => p.Notes)
            .HasMaxLength(1000);
    }
}