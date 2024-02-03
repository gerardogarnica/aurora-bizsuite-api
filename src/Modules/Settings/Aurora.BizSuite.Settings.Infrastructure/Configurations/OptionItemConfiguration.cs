namespace Aurora.BizSuite.Settings.Infrastructure.Configurations;

class OptionItemConfiguration : IEntityTypeConfiguration<OptionItem>
{
    public void Configure(EntityTypeBuilder<OptionItem> builder)
    {
        builder.ToTable("OptionItem", SettingsContext.DEFAULT_SCHEMA);

        builder.HasKey(oi => oi.Id)
            .HasName("PK_OptionItem");

        builder.Property(oi => oi.Id)
            .UseHiLo("optionitemseq", SettingsContext.DEFAULT_SCHEMA)
            .HasConversion(id => id.Value, value => new OptionItemId(value))
            .HasColumnName("OptionItemId")
            .IsRequired();

        builder.Property(oi => oi.OptionId)
            .HasColumnName("OptionId")
            .HasConversion(id => id.Value, value => new OptionId(value))
            .IsRequired();

        builder.Property(oi => oi.Code)
            .IsRequired()
            .HasMaxLength(40);

        builder.Property(oi => oi.Description)
            .HasMaxLength(200);

        builder.Property(oi => oi.IsActive)
            .IsRequired();
    }
}