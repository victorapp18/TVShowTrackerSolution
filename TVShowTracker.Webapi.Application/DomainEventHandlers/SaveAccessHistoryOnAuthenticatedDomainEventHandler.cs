using TVShowTracker.Webapi.Domain.Aggregates.IdentityAggregate;
using TVShowTracker.Webapi.Domain.DomainEvents;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Framework.Data.Abstractions.UnitOfWork;

namespace TVShowTracker.Webapi.Application.DomainEventHandlers
{
    public class SaveAccessHistoryOnAuthenticatedDomainEventHandler : INotificationHandler<AccessHistoryAuthenticatedDomainEvent>
    {
        private IIdentityRepository IdentityRepository { get; }
        private IUnitOfWork UnitOfWork { get; }

        public SaveAccessHistoryOnAuthenticatedDomainEventHandler(IIdentityRepository identityRepository,
                                                                  IUnitOfWork unitOfWork) 
        {
            IdentityRepository = identityRepository;
            UnitOfWork = unitOfWork;
        }

        public async Task Handle(AccessHistoryAuthenticatedDomainEvent notification, CancellationToken cancellationToken)
        {
            Identity identity = notification.Identity;
            AccessHistory accessHistory = AccessHistory.Create(identity.IdentityId, 
                                                               notification.Message, 
                                                               notification.Result);

            identity.SetAccessHistory(accessHistory);
            IdentityRepository.Update(identity);
            await UnitOfWork.CommitAsync();
        }
    }
}
