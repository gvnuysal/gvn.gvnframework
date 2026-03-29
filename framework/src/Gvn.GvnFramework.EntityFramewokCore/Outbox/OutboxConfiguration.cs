using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gvn.GvnFramework.EntityFramewokCore.Outbox;

/// <summary>
/// EF Core fluent configuration for the <see cref="OutboxMessage"/> entity.
/// Defines table name, column constraints, and indexes.
/// </summary>
public sealed class OutboxConfiguration : IEntityTypeConfiguration<OutboxMessage>
{
    /// <summary>
    /// Configures the <see cref="OutboxMessage"/> entity type.
    /// </summary>
    /// <param name="builder">The builder used to configure the entity type.</param>
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
