using TVShowTracker.Webapi.Domain.Aggregates.ChannelAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TVShowTracker.Webapi.Domain.Aggregates.ProgramAggregate;

namespace TVShowTracker.Webapi.Infrastructure.EntityTypeConfigurations
{
    public class ProgramEntityTypeConfiguration : IEntityTypeConfiguration<Program>
    {
        public void Configure(EntityTypeBuilder<Program> builder)
        {
            builder.ToTable("Program");

            builder.HasKey(p => p.ProgramId);
            builder.Property(p => p.ProgramId).HasColumnName("ProgramId").HasColumnType("CHAR(36)");
            builder.Property(p => p.ChannelId).HasColumnName("ChannelId").HasColumnType("CHAR(36)");
            builder.Property(p => p.GenderId).HasColumnName("GenderId");
            builder.Property(p => p.Name).HasColumnName("Name");
            builder.Property(p => p.Description).HasColumnName("Description");
            builder.Property(p => p.ExhibitionDate).HasColumnName("ExhibitionDate");
            builder.Property(p => p.CreateDate).HasColumnName("CreateDate");
            builder.Property(p => p.UpdateDate).HasColumnName("UpdateDate");

            builder.Ignore(it => it.Id);
            builder.Ignore(it => it.RequestedHashCode);
            builder.Ignore(it => it.Operation);

            builder.HasOne(p => p.Channel).WithMany(p => p.Programs).HasForeignKey(p => p.ChannelId);
            builder.HasOne(p => p.Gender).WithMany(p => p.Programs).HasForeignKey(p => p.GenderId);
        }
    }
}
