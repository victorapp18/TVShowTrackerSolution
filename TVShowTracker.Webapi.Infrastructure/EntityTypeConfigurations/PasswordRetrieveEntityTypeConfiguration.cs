using TVShowTracker.Webapi.Domain.Aggregates.IdentityAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TVShowTracker.Webapi.Infrastructure.EntityTypeConfigurations
{
    public class PasswordRetrieveEntityTypeConfiguration : IEntityTypeConfiguration<PasswordRetrieve>
    {
        public void Configure(EntityTypeBuilder<PasswordRetrieve> builder)
        {
            builder.ToTable("PasswordRetrieve");

            builder.HasKey(p => p.PasswordRetrieveId);
            builder.Property(p => p.PasswordRetrieveId).HasColumnName("PasswordRetrieveId").HasColumnType("CHAR(36)");
            builder.Property(p => p.IdentityId).HasColumnName("IdentityId").HasColumnType("CHAR(36)");
            builder.Property(p => p.Token).HasColumnName("Token").HasMaxLength(2000);
            builder.Property(p => p.CreateDate).HasColumnName("CreateDate");
            builder.Property(p => p.ExpirationDate).HasColumnName("ExpirationDate");
            builder.Property(p => p.UpdateDate).HasColumnName("UpdateDate");
            builder.Property(p => p.IsChanged).HasColumnName("IsChanged");
            builder.Property(p => p.PasswordProvisional).HasColumnName("PasswordProvisional");

            builder.Ignore(it => it.Id);
            builder.Ignore(it => it.RequestedHashCode);
            builder.Ignore(it => it.Operation);

            builder.HasOne(p => p.Identity).WithMany(p => p.PasswordRetrieves).HasForeignKey(p => p.IdentityId);
        }
    }
}
