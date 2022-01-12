using System;
using Framework.Seedworks.Domains.Abstraction;

namespace TVShowTracker.Webapi.Domain.Aggregates.IdentityAggregate
{
    public class IdentityRole : Entity
    {
        public Guid IdentityId { get; private set; }
        public int RoleId { get; private set; }

        public Identity Identity { get; private set; }
        public Role Role { get; private set; }

        public static IdentityRole Create(int roleId)
        {
            return new IdentityRole() { RoleId = roleId };
        }

        public static IdentityRole Create(Identity identity, Role role) 
        {
            return new IdentityRole() { Identity = identity, Role = role };
        }
    }
}
