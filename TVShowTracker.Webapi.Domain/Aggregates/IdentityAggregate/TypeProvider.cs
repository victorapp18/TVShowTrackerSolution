using System;
using System.Collections.Generic;
using Framework.Seedworks.Domains.Abstraction;

namespace TVShowTracker.Webapi.Domain.Aggregates.IdentityAggregate
{
    public class TypeProvider : Entity, IAggregateRoot
    {
        public int TypeProviderId { get; private set; }
        public string Name { get; private set; }
        public ICollection<Provider> Providers { get; private set; } = new List<Provider>();
    }
}
