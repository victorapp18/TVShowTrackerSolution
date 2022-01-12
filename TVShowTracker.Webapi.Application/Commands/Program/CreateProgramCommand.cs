using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using Framework.Message.Concrete;

namespace TVShowTracker.Webapi.Application.Commands.Program
{
    public class CreateProgramCommand : ApplicationRequest, IRequest<ApplicationResult<bool>>
    {
        public CreateProgramCommand() { }

        public Guid ChannelId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime ExhibitionDate { get; set; }
        public int GenderId { get; set; }

    }
}
