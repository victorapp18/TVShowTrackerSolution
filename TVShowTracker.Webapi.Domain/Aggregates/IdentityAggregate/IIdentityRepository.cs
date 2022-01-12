using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace TVShowTracker.Webapi.Domain.Aggregates.IdentityAggregate
{
    public interface IIdentityRepository
    {
        void Create(Identity model);
        Identity Get(string username);
        Identity GetInternal(string username, string password);
        PasswordRetrieve GetPasswordProvisional(string username, string passwordProvisional);
        Identity GetExternal(string username, string password, int typeProviderId);
        TypeProvider GetTypeProvider(string name);
        Identity Get(Guid identityId);
        Identity Get(Guid identityId, params Expression<Func<Identity, object>>[] includes);
        Role GetRoleById(int roleId);
        void Update(Identity model);
        void CreateProvider(Provider model);
        IdentityRole GetMyRoleByIdentityId(Guid identityId);
    }
}
