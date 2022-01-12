using Framework.Seedworks.Domains.Abstraction;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using TVShowTracker.Webapi.Domain.Aggregates.ChannelAggregate;

namespace TVShowTracker.Webapi.Domain.Aggregates.ProgramAggregate
{
    public class Program : Entity, IAggregateRoot
    {
        public Guid ProgramId { get; private set; }
        public Guid ChannelId { get; private set; }
        public int GenderId { get; private set; }
        public string Name { get; private set; }
        public string Description { get; set; }
        public DateTime ExhibitionDate { get; private set; }
        public DateTime CreateDate { get; private set; }
        public DateTime UpdateDate { get; private set; }
        public Gender Gender{ get; private set; }
        public Channel Channel { get; private set; }
        public static Program Create(Guid channelId, int genderId, string name, string description, DateTime exhibitionDate)
        {
            Program program = new Program
            {
                ProgramId = Guid.NewGuid(),
                ChannelId = channelId,
                GenderId = genderId,
                Name = name,
                Description = description,
                ExhibitionDate = exhibitionDate,
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now
            };
            return program;
        }
    }
}
