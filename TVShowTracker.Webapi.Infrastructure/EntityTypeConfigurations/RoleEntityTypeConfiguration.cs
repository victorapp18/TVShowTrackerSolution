using TVShowTracker.Webapi.Domain.Aggregates.IdentityAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TVShowTracker.Webapi.Infrastructure.EntityTypeConfigurations
{
    public class RoleEntityTypeConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Role");

            builder.HasKey(p => p.RoleId);
            builder.Property(p => p.RoleId).HasColumnName("RoleId");
            builder.Property(p => p.Name).HasColumnName("Name").HasMaxLength(500);
            builder.Property(p => p.Description).HasColumnName("Description").HasMaxLength(2000);

            builder.Ignore(it => it.Id);
            builder.Ignore(it => it.RequestedHashCode);
            builder.Ignore(it => it.Operation);

            builder.HasMany(p => p.IdentityRoles).WithOne(p => p.Role).HasForeignKey(p => p.RoleId);
        }
    }
}
