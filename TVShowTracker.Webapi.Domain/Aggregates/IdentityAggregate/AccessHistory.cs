using System;
using Framework.Seedworks.Domains.Abstraction;

namespace TVShowTracker.Webapi.Domain.Aggregates.IdentityAggregate
{
    public class AccessHistory : Entity
    {
        public Guid AccessHistoryId { get; private set; }
        public DateTime CreateDate { get; private set; }
        public string Message { get; private set; }
        public Guid IdentityId { get; private set; }
        public bool Result { get; set; }
        public Identity Identity { get; private set; }

        public static AccessHistory Create(Guid identityId, string message, bool result) 
        {
            return new AccessHistory()
            {
                CreateDate = DateTime.Now,
                Message = message,
                IdentityId = identityId,
                Result = result
            };
        }
    }
}
