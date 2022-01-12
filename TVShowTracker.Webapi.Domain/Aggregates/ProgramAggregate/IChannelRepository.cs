using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace TVShowTracker.Webapi.Domain.Aggregates.ProgramAggregate
{
    public interface IProgramRepository
    {
        void Create(Program model);
        Program Get(DateTime ExhibitionDate, Guid ChannelId);

    }
}
