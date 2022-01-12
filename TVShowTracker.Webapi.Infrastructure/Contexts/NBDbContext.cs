using TVShowTracker.Webapi.Domain.Aggregates.IdentityAggregate;
using TVShowTracker.Webapi.Domain.Aggregates.ChannelAggregate;
using TVShowTracker.Webapi.Infrastructure.EntityTypeConfigurations;
using Microsoft.EntityFrameworkCore;
using System;
using Framework.Data.Abstractions.DbCotexts;
using TVShowTracker.Webapi.Domain.Aggregates.ProgramAggregate;

namespace TVShowTracker.Webapi.Infrastructure.Contexts
{
    public class NBDbContext : MySqlDbContext, IDisposable
    {
        private bool disposed = false;

        public NBDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Channel> Channels { get; private set; }
        public DbSet<Program> Programs { get; private set; }
        public DbSet<Gender> Genders { get; private set; }
        public DbSet<Identity> Identities { get; private set; }
        public DbSet<Provider> Providers { get; private set; }
        public DbSet<TypeProvider> TypeProviders { get; private set; }
        public DbSet<Role> Roles { get; private set; }
        public DbSet<IdentityRole> IdentityRoles { get; private set; }
        public DbSet<AccessHistory> AccessHistories { get; private set; }
        public DbSet<PasswordRetrieve> PasswordRetrieves { get; private set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ProgramEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new GenderEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ChannelEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new IdentityEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new RoleEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new IdentityRoleEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new AccessHistoryEntityTypeConfigueration());
            modelBuilder.ApplyConfiguration(new PasswordRetrieveEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ProviderEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new TypeProviderEntityTypeConfiguration());
        }

        public override void Dispose()
        {
            base.Dispose();
            Dispose(true);
        }

        private void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                Channels = null;
                Identities = null;
                Roles = null;
                IdentityRoles = null;
                AccessHistories = null;
            }

            disposed = true;
            GC.Collect();
        }
    }
}
