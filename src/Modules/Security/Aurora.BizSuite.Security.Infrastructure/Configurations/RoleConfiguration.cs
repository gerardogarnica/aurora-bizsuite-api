namespace Aurora.BizSuite.Security.Infrastructure.Configurations;

internal class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("Role", SecurityContext.DEFAULT_SCHEMA);

        builder.HasKey(k => k.Id)
            .HasName("PK_Role");

        builder.Property(p => p.Id)
            .HasColumnName("RoleId")
            .HasConversion(id => id.Value, value => new RoleId(value))
            .IsRequired();

        builder.Property(p => p.Name).IsRequired().HasMaxLength(50);
        builder.Property(p => p.Description).IsRequired().HasMaxLength(200);
        builder.Property(p => p.Notes).HasMaxLength(1000);
        builder.Property(p => p.IsActive).IsRequired();

        builder.AddAuditableProperties<Role, RoleId>();

        // Indexes
        builder.HasIndex(p => p.Name)
            .IsUnique()
            .HasDatabaseName("UK_Role");
    }
}