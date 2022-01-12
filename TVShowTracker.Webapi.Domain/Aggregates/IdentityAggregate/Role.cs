using System.Collections.Generic;
using Framework.Seedworks.Domains.Abstraction;

namespace TVShowTracker.Webapi.Domain.Aggregates.IdentityAggregate
{
    public class Role : Entity
    {
        public int RoleId { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public ICollection<IdentityRole> IdentityRoles { get; private set; }
    }
}
