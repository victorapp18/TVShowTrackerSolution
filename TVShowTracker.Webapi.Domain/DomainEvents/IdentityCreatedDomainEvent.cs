using TVShowTracker.Webapi.Domain.Aggregates.IdentityAggregate;
using MediatR;

namespace TVShowTracker.Webapi.Domain.DomainEvents
{
    public class IdentityCreatedDomainEvent : INotification
    {
        public Identity Identity { get; }
        public string Password { get; }
        public string Language { get; }

        public IdentityCreatedDomainEvent(Identity identity, string password, string language) 
        {
            Identity = identity;
            Password = password;
            Language = language;
        }
    }
}
