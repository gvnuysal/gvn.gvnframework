using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gvn.GvnFramework.EntityFramewokCore.Outbox;

public sealed class OutboxConfiguration : IEntityTypeConfiguration<OutboxMessage>
{
    public void Configure(EntityTypeBuilder<OutboxMessage> builder)
    {
        builder.ToTable("OutboxMessages");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.EventType).HasMaxLength(500).IsRequired();
        builder.Property(x => x.Content).HasColumnType("nvarchar(max)").IsRequired();
        builder.Property(x => x.Error).HasMaxLength(2000);
        builder.HasIndex(x => x.ProcessedAt);
    }
}
