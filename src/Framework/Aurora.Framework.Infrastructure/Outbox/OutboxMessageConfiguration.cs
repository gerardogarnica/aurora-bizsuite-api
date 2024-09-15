using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aurora.Framework.Infrastructure.Outbox;

public sealed class OutboxMessageConfiguration : IEntityTypeConfiguration<OutboxMessage>
{
    public void Configure(EntityTypeBuilder<OutboxMessage> builder)
    {
        // Table name
        builder.ToTable("OutboxMessages");

        // Keys
        builder.HasKey(e => e.Id).HasName("PK_OutboxMessages");

        // Properties
        builder.Property(p => p.Id).HasColumnName("OutboxId");
        builder.Property(p => p.Type).HasMaxLength(100);
        builder.Property(p => p.Content).HasMaxLength(4000);
        builder.Property(p => p.Error).HasMaxLength(4000);
    }
}