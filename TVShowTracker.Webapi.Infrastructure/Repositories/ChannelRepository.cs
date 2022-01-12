using TVShowTracker.Webapi.Domain.Aggregates.ChannelAggregate;
using TVShowTracker.Webapi.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace TVShowTracker.Webapi.Infrastructure.Repositories
{
    public class ChannelRepository : IChannelRepository, IDisposable
    {
        private bool disposed = false;

        private NBDbContext Context { get; }

        public ChannelRepository(NBDbContext context) => Context = context;

        public void Create(Channel model) => Context.Channels.Add(model);
        public Channel Get(int namber)
        {
            return Context.Channels
                          .FirstOrDefault(it => it.Namber == namber);
        }
        public Channel Get(Guid channelId)
        {
            return Context.Channels
                          .FirstOrDefault(it => it.ChannelId == channelId);
        }
        public void Dispose()
        {
            Dispose(true);
        }

        private void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                Context.Dispose();
            }

            disposed = true;
            GC.Collect();
        }
    }
}
