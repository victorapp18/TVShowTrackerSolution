using Framework.Seedworks.Domains.Abstraction;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using TVShowTracker.Webapi.Domain.Aggregates.ProgramAggregate;

namespace TVShowTracker.Webapi.Domain.Aggregates.ChannelAggregate
{
    public class Channel : Entity, IAggregateRoot
    {
        public Guid ChannelId { get; private set; }
        public string Name { get; private set; }
        public int Namber{ get; private set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; private set; }
        public DateTime UpdateDate { get; private set; }
        public ICollection<Program> Programs { get; private set; } = new List<Program>();
        public static Channel Create(string name, string description, int namber)
        {
            Channel channel = new Channel
            {
                ChannelId = Guid.NewGuid(),
                Name = name,
                Namber = namber,
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now,
                Description = description
            };


            return channel;
        }
    }
}
