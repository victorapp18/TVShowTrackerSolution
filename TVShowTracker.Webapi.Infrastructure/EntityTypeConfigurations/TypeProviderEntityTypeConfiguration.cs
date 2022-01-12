using TVShowTracker.Webapi.Domain.Aggregates.IdentityAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TVShowTracker.Webapi.Infrastructure.EntityTypeConfigurations
{
    public class TypeProviderEntityTypeConfiguration : IEntityTypeConfiguration<TypeProvider>
    {
        public void Configure(EntityTypeBuilder<TypeProvider> builder)
        {
            builder.ToTable("TypeProvider");

            builder.HasKey(p => p.TypeProviderId);
            builder.Property(p => p.TypeProviderId).HasColumnName("TypeProviderId");
            builder.Property(p => p.Name).HasColumnName("Name").HasMaxLength(45);
            
            builder.Ignore(it => it.Id);
            builder.Ignore(it => it.RequestedHashCode);
            builder.Ignore(it => it.Operation);

            builder.HasMany(p => p.Providers).WithOne(p => p.TypeProvider).HasForeignKey(p => p.TypeProviderId);
        }
    }
}
