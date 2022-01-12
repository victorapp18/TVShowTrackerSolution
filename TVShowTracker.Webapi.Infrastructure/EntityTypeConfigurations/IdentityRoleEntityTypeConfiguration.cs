using TVShowTracker.Webapi.Domain.Aggregates.IdentityAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TVShowTracker.Webapi.Infrastructure.EntityTypeConfigurations
{
    public class IdentityRoleEntityTypeConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.ToTable("IdentityRole");

            builder.HasKey(p => new { p.IdentityId, p.RoleId });
            builder.Property(p => p.IdentityId).HasColumnName("IdentityId").HasColumnType("CHAR(36)");
            builder.Property(p => p.RoleId).HasColumnName("RoleId");

            builder.Ignore(it => it.Id);
            builder.Ignore(it => it.RequestedHashCode);
            builder.Ignore(it => it.Operation);

            builder.HasOne(p => p.Identity).WithMany(p => p.IdentityRoles).HasForeignKey(p => p.IdentityId);
            builder.HasOne(p => p.Role).WithMany(p => p.IdentityRoles).HasForeignKey(p => p.RoleId);
        }
    }
}
