using TVShowTracker.Webapi.Domain.Aggregates.IdentityAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TVShowTracker.Webapi.Infrastructure.EntityTypeConfigurations
{
    public class AccessHistoryEntityTypeConfigueration : IEntityTypeConfiguration<AccessHistory>
    {
        public void Configure(EntityTypeBuilder<AccessHistory> builder)
        {
            builder.ToTable("AccessHistory");

            builder.HasKey(p => p.AccessHistoryId);
            builder.Property(p => p.AccessHistoryId).HasColumnName("AccessHistoryId").HasColumnType("CHAR(36)");
            builder.Property(p => p.CreateDate).HasColumnName("CreateDate");
            builder.Property(p => p.Result).HasColumnName("Result");
            builder.Property(p => p.IdentityId).HasColumnName("IdentityId");
            builder.Property(p => p.Message).HasColumnName("Message");

            builder.Ignore(it => it.Id);
            builder.Ignore(it => it.RequestedHashCode);
            builder.Ignore(it => it.Operation);

            builder.HasOne(p => p.Identity).WithMany(p => p.AccessHistories).HasForeignKey(p => p.IdentityId);
        }
    }
}
