using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using Framework.Message.Concrete;

namespace TVShowTracker.Webapi.Application.Commands.Channel
{
    public class CreateChannelCommand : ApplicationRequest, IRequest<ApplicationResult<bool>>
    {
        public CreateChannelCommand() { }

        public string Name { get; set; }
        public int Namber { get; set; }
        public string Description { get; set; }
    }
}
