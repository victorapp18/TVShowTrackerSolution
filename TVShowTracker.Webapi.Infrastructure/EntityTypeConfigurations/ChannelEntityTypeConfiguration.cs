using TVShowTracker.Webapi.Domain.Aggregates.ChannelAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TVShowTracker.Webapi.Infrastructure.EntityTypeConfigurations
{
    public class ChannelEntityTypeConfiguration : IEntityTypeConfiguration<Channel>
    {
        public void Configure(EntityTypeBuilder<Channel> builder)
        {
            builder.ToTable("Channel");

            builder.HasKey(p => p.ChannelId);
            builder.Property(p => p.ChannelId).HasColumnName("ChannelId").HasColumnType("CHAR(36)");
            builder.Property(p => p.Name).HasColumnName("Name").HasMaxLength(500);
            builder.Property(p => p.Description).HasColumnName("Description").HasMaxLength(200);
            builder.Property(p => p.Namber).HasColumnName("Namber");
            builder.Property(p => p.CreateDate).HasColumnName("CreateDate");
            builder.Property(p => p.UpdateDate).HasColumnName("UpdateDate");

            builder.Ignore(it => it.Id);
            builder.Ignore(it => it.RequestedHashCode);
            builder.Ignore(it => it.Operation);
        }
    }
}
