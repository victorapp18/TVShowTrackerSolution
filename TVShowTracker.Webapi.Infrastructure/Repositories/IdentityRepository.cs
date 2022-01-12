using TVShowTracker.Webapi.Domain.Aggregates.IdentityAggregate;
using TVShowTracker.Webapi.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace TVShowTracker.Webapi.Infrastructure.Repositories
{
    public class IdentityRepository : IIdentityRepository, IDisposable
    {
        private bool disposed = false;

        private NBDbContext Context { get; }

        public IdentityRepository(NBDbContext context) => Context = context;

        public void Create(Identity model) => Context.Identities.Add(model);
        public void CreateProvider(Provider model) => Context.Providers.Add(model);
        
        public TypeProvider GetTypeProvider(string name)
        {
            return Context.TypeProviders
                          .FirstOrDefault(it => it.Name == name);
        }
        public Identity Get(string email)
        {
            return Context.Identities
                          .Include(p => p.IdentityRoles)
                          .ThenInclude(p => p.Role)
                          .FirstOrDefault(it => it.Username == email && !it.IsAccessExternal);
        }
        public Identity GetExternal(string email, string password, int typeProviderId)
        {
            return Context.Identities
                          .Include(p => p.IdentityRoles)
                          .ThenInclude(p => p.Role)
                          .Include(c => c.Providers.Where(E=>E.TypeProviderId == typeProviderId))
                          .FirstOrDefault(it => it.Username == email &&
                            it.Password == password);
            
        }
        public Identity GetInternal(string email, string password)
        {
            return Context.Identities
                          .Include(p => p.IdentityRoles)
                          .ThenInclude(p => p.Role)
                          .FirstOrDefault(it => it.Username == email && it.Password == password && !it.IsAccessExternal);
        }
        public PasswordRetrieve GetPasswordProvisional(string email, string passwordProvisional)
        {
            return Context.PasswordRetrieves
                          .Include(p => p.Identity)
                          .FirstOrDefault(it => it.Identity.Username == email 
                                                && it.Identity.IdentityId == it.IdentityId 
                                                && it.PasswordProvisional == passwordProvisional
                                                && !it.IsChanged);
        }
        public Identity Get(Guid identityId)
        {
            return Context.Identities
                          .Include(it => it.PasswordRetrieves)
                          .FirstOrDefault(it => it.IdentityId == identityId);
        }

        public Identity Get(Guid identityId, params Expression<Func<Identity, object>>[] includes)
        {
            IQueryable<Identity> query = Context.Identities
                                                .Where(it => it.IdentityId == identityId)
                                                .AsQueryable();

            includes.ToList().ForEach(it => query = query.Include(it));

            return query.FirstOrDefault();
        }

        public IdentityRole GetMyRoleByIdentityId(Guid identityId)
        {
            return Context.IdentityRoles
                          .FirstOrDefault(it => it.IdentityId == identityId);
        }

        public void Update(Identity model)
        {
            Context.Update(model);
        }

        public Role GetRoleById(int roleId) => Context.Roles.FirstOrDefault(it => it.RoleId == roleId);

        public void Dispose()
        {
            Dispose(true);
        }

        private void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                Context.Dispose();
            }

            disposed = true;
            GC.Collect();
        }


    }
}
