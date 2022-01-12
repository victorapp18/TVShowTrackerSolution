using TVShowTracker.Webapi.Domain.Aggregates.IdentityAggregate;
using MediatR;

namespace TVShowTracker.Webapi.Domain.DomainEvents
{
    public class AccessHistoryAuthenticatedDomainEvent : INotification
    {
        public Identity Identity { get; }
        public bool Result { get; }
        public string Message { get; }

        public AccessHistoryAuthenticatedDomainEvent(Identity identity, bool result, string message) 
        {
            Identity = identity;
            Result = result;
            Message = message;
        }
    }
}
