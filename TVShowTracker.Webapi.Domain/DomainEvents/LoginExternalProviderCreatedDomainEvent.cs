using TVShowTracker.Webapi.Domain.Aggregates.IdentityAggregate;
using MediatR;
using System;

namespace TVShowTracker.Webapi.Domain.DomainEvents
{
    public class LoginExternalProviderCreatedDomainEvent : INotification
    {
        public Guid IdentityId { get; }
        public string Description { get; }

        public LoginExternalProviderCreatedDomainEvent(Guid identityId, string description) 
        {
            IdentityId = identityId;
            Description = description;
        }
    }
}
