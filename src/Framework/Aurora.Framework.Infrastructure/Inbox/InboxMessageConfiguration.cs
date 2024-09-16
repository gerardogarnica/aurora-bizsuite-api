using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aurora.Framework.Infrastructure.Inbox;

public sealed class InboxMessageConfiguration : IEntityTypeConfiguration<InboxMessage>
{
    public void Configure(EntityTypeBuilder<InboxMessage> builder)
    {
        // Table name
        builder.ToTable("InboxMessages");

        // Keys
        builder.HasKey(e => e.Id).HasName("PK_InboxMessages");

        // Properties
        builder.Property(p => p.Id).HasColumnName("InboxId");
        builder.Property(p => p.Type).HasMaxLength(100);
        builder.Property(p => p.Content).HasMaxLength(4000);
        builder.Property(p => p.Error).HasMaxLength(4000);
    }
}