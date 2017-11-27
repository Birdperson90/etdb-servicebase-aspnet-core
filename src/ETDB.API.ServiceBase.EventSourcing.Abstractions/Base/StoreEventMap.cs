using ETDB.API.ServiceBase.EventSourcing.Abstractions.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ETDB.API.ServiceBase.EventSourcing.Abstractions.Base
{
    public class StoreEventMap : IEntityTypeConfiguration<StoredEvent>
    {
        public void Configure(EntityTypeBuilder<StoredEvent> builder)
        {
            var tableName = $"{nameof(StoredEvent)}s";

            builder.ToTable(tableName);

            builder.Property(c => c.Timestamp)
                .HasColumnName("CreationDate");

            builder.Property(c => c.MessageType)
                .HasColumnName("Action")
                .HasColumnType("varchar(100)");

            builder.Ignore(c => c.Type);
        }
    }
}
