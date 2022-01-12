using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace TVShowTracker.Webapi.Domain.Aggregates.ChannelAggregate
{
    public interface IChannelRepository
    {
        void Create(Channel model);
        Channel Get(int namber);
        Channel Get(Guid channelId);

    }
}
