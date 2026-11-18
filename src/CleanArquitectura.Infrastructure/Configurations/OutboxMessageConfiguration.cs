
using CleanArquitectura.Infrastructure.Outbox;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArquitectura.Infrastructure.Configurations;

public class OutboxMessageConfiguration : IEntityTypeConfiguration<OutboxMessage>
{
    public void Configure(EntityTypeBuilder<OutboxMessage> builder)
    {
        builder.ToTable("outbox_messages");
        builder.HasKey(outboxMesage => outboxMesage.Id);

        builder.Property(outboxMessage => outboxMessage.Content)
            .HasColumnType("jsonb");
    }
}
