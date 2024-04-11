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

        builder.Property(p => p.ApplicationId).IsRequired();
        builder.Property(p => p.Name).IsRequired().HasMaxLength(50);
        builder.Property(p => p.Description).IsRequired().HasMaxLength(200);
        builder.Property(p => p.Notes).HasMaxLength(1000);

        builder.AddAuditableProperties<Role, RoleId>();
        builder.AddSoftDeletableProperties();

        // Query filters
        builder.HasQueryFilter(f => !f.IsDeleted);
        builder
            .HasIndex(i => i.IsDeleted)
            .HasFilter("IsDeleted = 0")
            .HasDatabaseName("IX_Role_IsDeleted");

        // Indexes
        builder.HasIndex(i => new { i.ApplicationId, i.Name })
            .IsUnique()
            .HasDatabaseName("UK_Role");

        builder
            .HasIndex(i => i.ApplicationId)
            .HasDatabaseName("IX_Role_ApplicationId");

        // Foreign keys
        builder
            .HasOne<Application>()
            .WithMany(n => n.Roles)
            .HasForeignKey(f => f.ApplicationId)
            .IsRequired()
            .HasConstraintName("FK_Role_Application");
    }
}