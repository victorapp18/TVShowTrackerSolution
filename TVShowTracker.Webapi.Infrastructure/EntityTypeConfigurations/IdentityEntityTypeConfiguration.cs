using TVShowTracker.Webapi.Domain.Aggregates.IdentityAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TVShowTracker.Webapi.Infrastructure.EntityTypeConfigurations
{
    public class IdentityEntityTypeConfiguration : IEntityTypeConfiguration<Identity>
    {
        public void Configure(EntityTypeBuilder<Identity> builder)
        {
            builder.ToTable("Identity");

            builder.HasKey(p => p.IdentityId);
            builder.Property(p => p.IdentityId).HasColumnName("IdentityId").HasColumnType("CHAR(36)");
            builder.Property(p => p.Name).HasColumnName("Name").HasMaxLength(500);
            builder.Property(p => p.Username).HasColumnName("Username").HasMaxLength(200);
            builder.Property(p => p.CreateDate).HasColumnName("CreateDate");
            builder.Property(p => p.IsFirstAccess).HasColumnName("IsFirstAccess");
            builder.Property(p => p.Password).HasColumnName("Password");
            builder.Property(p => p.UpdateDate).HasColumnName("UpdateDate");
            builder.Property(p => p.IsActive).HasColumnName("IsActive"); 
            builder.Property(p => p.IsAccessExternal).HasColumnName("IsAccessExternal");
            builder.Property(p => p.Contact).HasColumnName("Contact");

            builder.Ignore(it => it.Id);
            builder.Ignore(it => it.RequestedHashCode);
            builder.Ignore(it => it.Operation);

            builder.HasMany(p => p.IdentityRoles).WithOne(p => p.Identity).HasForeignKey(p => p.IdentityId);
            builder.HasMany(p => p.AccessHistories).WithOne(p => p.Identity).HasForeignKey(p => p.IdentityId);
            builder.HasMany(p => p.PasswordRetrieves).WithOne(p => p.Identity).HasForeignKey(p => p.IdentityId);
        }
    }
}
