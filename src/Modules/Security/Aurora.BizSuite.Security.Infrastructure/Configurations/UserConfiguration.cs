namespace Aurora.BizSuite.Security.Infrastructure.Configurations;

internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("User", SecurityContext.DEFAULT_SCHEMA);

        builder.HasKey(k => k.Id)
            .HasName("PK_User");

        builder.Property(p => p.Id)
            .HasColumnName("UserId")
            .HasConversion(id => id.Value, value => new UserId(value))
            .IsRequired();

        builder.Property(p => p.FirstName).IsRequired().HasMaxLength(20);
        builder.Property(p => p.LastName).IsRequired().HasMaxLength(20);
        builder.Property(p => p.Email).IsRequired().HasMaxLength(50);

        builder.Property<string>("_passwordHash")
            .HasField("_passwordHash")
            .HasColumnName("PasswordHash")
            .IsRequired()
            .HasMaxLength(256);

        builder.Property(p => p.PasswordExpirationDate);
        builder.Property(p => p.Notes).HasMaxLength(1000);
        builder.Property(p => p.IsEditable).IsRequired();
        builder.Property(p => p.Status).IsRequired();

        builder.AddAuditableProperties<User, UserId>();

        builder.Ignore(user => user.FullName);

        // Indexes
        builder.HasIndex(p => p.Email)
            .IsUnique()
            .HasDatabaseName("UK_User");
    }
}