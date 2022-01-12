using TVShowTracker.Webapi.Domain.Aggregates.IdentityAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TVShowTracker.Webapi.Domain.Aggregates.ProgramAggregate;

namespace TVShowTracker.Webapi.Infrastructure.EntityTypeConfigurations
{
    public class GenderEntityTypeConfiguration : IEntityTypeConfiguration<Gender>
    {
        public void Configure(EntityTypeBuilder<Gender> builder)
        {
            builder.ToTable("Gender");

            builder.HasKey(p => p.GenderId);
            builder.Property(p => p.GenderId).HasColumnName("GenderId");
            builder.Property(p => p.Name).HasColumnName("Name").HasMaxLength(500);
            builder.Property(p => p.Description).HasColumnName("Description").HasMaxLength(2000);

            builder.Ignore(it => it.Id);
            builder.Ignore(it => it.RequestedHashCode);
            builder.Ignore(it => it.Operation);

            builder.HasMany(p => p.Programs).WithOne(p => p.Gender).HasForeignKey(p => p.GenderId);
        }
    }
}
