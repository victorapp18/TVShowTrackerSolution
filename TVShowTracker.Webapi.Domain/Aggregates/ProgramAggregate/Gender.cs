using Framework.Seedworks.Domains.Abstraction;
using System.Collections.Generic;

namespace TVShowTracker.Webapi.Domain.Aggregates.ProgramAggregate
{
    public class Gender : Entity, IAggregateRoot
    {
        public int GenderId { get; private set; }
        public string Name { get; private set; }
        public string Description { get; set; }
        public ICollection<Program> Programs { get; private set; }
    }
}
