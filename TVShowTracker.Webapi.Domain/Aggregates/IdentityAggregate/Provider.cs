using System;
using System.Collections.Generic;
using Framework.Seedworks.Domains.Abstraction;

namespace TVShowTracker.Webapi.Domain.Aggregates.IdentityAggregate
{
    public class Provider : Entity
    {
        public Guid IdentityId { get; private set; }
        public int TypeProviderId { get; private set; }
        public Identity Identity { get; private set; }
        public TypeProvider TypeProvider { get; private set; }

        public static Provider Create(Guid identityId, int typeProviderId) 
        {
            return new Provider()
            {
                IdentityId = identityId,
                TypeProviderId = typeProviderId
            };
        }
    }
}
