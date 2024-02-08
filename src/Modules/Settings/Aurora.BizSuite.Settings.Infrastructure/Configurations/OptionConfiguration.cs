namespace Aurora.BizSuite.Settings.Infrastructure.Configurations;

internal class OptionConfiguration : IEntityTypeConfiguration<Option>
{
    public void Configure(EntityTypeBuilder<Option> builder)
    {
        builder.ToTable("Option", SettingsContext.DEFAULT_SCHEMA);

        builder.HasKey(o => o.Id)
            .HasName("PK_Option");

        builder.Property(o => o.Id)
            .HasColumnName("OptionId")
            .HasConversion(id => id.Value, value => new OptionId(value))
            .IsRequired()
            .ValueGeneratedOnAdd();

        builder.Property(o => o.Code)
            .IsRequired()
            .HasMaxLength(40);

        builder.Property(o => o.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(o => o.Description)
            .HasMaxLength(200);

        builder.Property(o => o.Type)
            .HasColumnType("tinyint")
            .IsRequired();

        builder.Property(o => o.CreatedBy)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(o => o.CreatedAt)
            .IsRequired();

        builder.Property(o => o.UpdatedBy)
            .HasMaxLength(100);

        builder.Property(o => o.UpdatedAt)
            .IsRequired(false);

        builder.HasIndex(o => o.Code)
            .IsUnique()
            .HasDatabaseName("UK_Option");

        builder.HasMany(o => o.Items)
            .WithOne()
            .HasForeignKey(oi => oi.OptionId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}