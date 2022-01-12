using TVShowTracker.Webapi.Domain.Aggregates.IdentityAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TVShowTracker.Webapi.Infrastructure.EntityTypeConfigurations
{
    public class ProviderEntityTypeConfiguration : IEntityTypeConfiguration<Provider>
    {
        public void Configure(EntityTypeBuilder<Provider> builder)
        {
            builder.ToTable("Provider");

            builder.HasKey(p => new { p.IdentityId, p.TypeProviderId });
            builder.Property(p => p.IdentityId).HasColumnName("IdentityId").HasColumnType("CHAR(36)");
            builder.Property(p => p.TypeProviderId).HasColumnName("TypeProviderId");

            builder.Ignore(it => it.Id);
            builder.Ignore(it => it.RequestedHashCode);
            builder.Ignore(it => it.Operation);

            builder.HasOne(p => p.Identity).WithMany(p => p.Providers).HasForeignKey(p => p.IdentityId);
            builder.HasOne(p => p.TypeProvider).WithMany(p => p.Providers).HasForeignKey(p => p.TypeProviderId);
        }
    }
}
