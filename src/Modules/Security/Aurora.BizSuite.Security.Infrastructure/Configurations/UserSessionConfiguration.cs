using Aurora.BizSuite.Security.Domain.Session;

namespace Aurora.BizSuite.Security.Infrastructure.Configurations;

internal class UserSessionConfiguration : IEntityTypeConfiguration<UserSession>
{
    public void Configure(EntityTypeBuilder<UserSession> builder)
    {
        builder.ToTable("UserSession", SecurityContext.DEFAULT_SCHEMA);

        builder.HasKey(k => k.Id)
            .HasName("PK_UserSession");

        builder.Property(p => p.Id)
            .HasColumnName("SessionId")
            .HasConversion(id => id.Value, value => new UserSessionId(value))
            .IsRequired();

        builder.Property(p => p.UserId)
            .HasColumnName("UserId")
            .HasConversion(id => id.Value, value => new UserId(value))
            .IsRequired();

        builder.Property(p => p.Application).IsRequired().HasMaxLength(50);
        builder.Property(p => p.AccessToken).IsRequired().HasMaxLength(4000);
        builder.Property(p => p.AccessTokenExpiration).IsRequired();
        builder.Property(p => p.RefreshToken).IsRequired().HasMaxLength(4000);
        builder.Property(p => p.RefreshTokenExpiration).IsRequired();
        builder.Property(p => p.BeginSessionDate).IsRequired();
        builder.Property(p => p.EndSessionDate);
        builder.AddAuditableProperties<UserSession, UserSessionId>();

        // Indexes
        builder
            .HasIndex(i => i.UserId)
            .HasDatabaseName("IX_UserSession_UserId");
    }
}