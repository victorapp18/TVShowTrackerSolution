using TVShowTracker.Webapi.Domain.Aggregates.IdentityAggregate;
using MediatR;

namespace TVShowTracker.Webapi.Domain.DomainEvents
{
    public class PasswordRetrieveCreatetedDomainEvent : INotification
    {
        public Identity Identity { get; }
        public string PasswordProvisional { get; }
        public string Language { get; }

        public PasswordRetrieveCreatetedDomainEvent(Identity identity, string passwordProvisional, string language) 
        {
            Identity = identity;
            PasswordProvisional = passwordProvisional;
            Language = language;
        }
    }
}
