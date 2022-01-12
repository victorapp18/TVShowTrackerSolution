using TVShowTracker.Webapi.Domain.Aggregates.ProgramAggregate;
using TVShowTracker.Webapi.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace TVShowTracker.Webapi.Infrastructure.Repositories
{
    public class ProgramRepository : IProgramRepository, IDisposable
    {
        private bool disposed = false;

        private NBDbContext Context { get; }

        public ProgramRepository(NBDbContext context) => Context = context;

        public void Create(Program model) => Context.Programs.Add(model);
        public Program Get(DateTime exhibitionDate, Guid channelId)
        {
            return Context.Programs
                          .FirstOrDefault(it => it.ChannelId == channelId && it.ExhibitionDate == exhibitionDate);
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
