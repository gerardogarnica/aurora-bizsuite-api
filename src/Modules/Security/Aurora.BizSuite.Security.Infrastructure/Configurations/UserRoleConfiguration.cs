namespace Aurora.BizSuite.Security.Infrastructure.Configurations;

internal class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.ToTable("UserRole", SecurityContext.DEFAULT_SCHEMA);

        builder.HasKey(k => k.Id)
            .HasName("PK_UserRole");

        builder.Property(p => p.Id)
            .HasColumnName("UserRoleId")
            .HasConversion(id => id.Value, value => new UserRoleId(value))
            .IsRequired()
            .UseIdentityColumn();

        builder.Property(p => p.UserId).IsRequired();
        builder.Property(p => p.RoleId).IsRequired();
        builder.Property(p => p.IsEditable).IsRequired();
        builder.AddAuditableProperties<UserRole, UserRoleId>();

        // Indexes
        builder
            .HasIndex(i => new { i.UserId, i.RoleId })
            .IsUnique()
            .HasDatabaseName("UK_UserRole");

        builder
            .HasIndex(i => i.UserId)
            .HasDatabaseName("IX_UserRole_UserId");

        // Foreign keys
        builder
            .HasOne<User>()
            .WithMany(n => n.Roles)
            .HasForeignKey(f => f.UserId)
            .IsRequired()
            .HasConstraintName("FK_UserRole_User");

        builder
            .HasOne<Role>()
            .WithMany(n => n.Users)
            .HasForeignKey(f => f.RoleId)
            .IsRequired()
            .HasConstraintName("FK_UserRole_Role");
    }
}