namespace Aurora.BizSuite.Security.Infrastructure.Configurations;

internal class ApplicationConfiguration : IEntityTypeConfiguration<Application>
{
    public void Configure(EntityTypeBuilder<Application> builder)
    {
        builder.ToTable("Application", SecurityContext.DEFAULT_SCHEMA);

        builder.HasKey(k => k.Id)
            .HasName("PK_Application");

        builder.Property(p => p.Id)
            .HasColumnName("ApplicationId")
            .HasConversion(id => id.Value, value => new Domain.Applications.ApplicationId(value))
            .IsRequired();

        builder.Property(p => p.Name).IsRequired().HasMaxLength(50);
        builder.Property(p => p.Description).IsRequired().HasMaxLength(200);
        builder.Property(p => p.HasCustomConfiguration).IsRequired();

        builder.AddAuditableProperties<Application, Domain.Applications.ApplicationId>();
    }
}