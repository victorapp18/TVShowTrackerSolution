using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Framework.Message.Abstraction;
using System.Collections.Generic;
using TVShowTracker.Webapi.Application.Commands.Channel;
using TVShowTracker.Webapi.Application.ViewModels.Channel;
using TVShowTracker.Webapi.Application.Queries.Channel;

namespace TVShowTracker.WebApi.Assets.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/channels")]
    public class ChannelController : Controller
    {
        private IMediator Mediator { get; }
        private IChannelQuery ChannelQuery { get; }

        public ChannelController(IMediator mediator, IChannelQuery channelQuery) 
        {
            Mediator = mediator;
            ChannelQuery = channelQuery;
        }

        [HttpGet, Produces("application/json", Type = typeof(IApplicationResult<IEnumerable<ChannelResultViewModel>>))]
        public async Task<IActionResult> GetMyAsync() => await ChannelQuery.GetAllAsync();

        [HttpPost, Produces("application/json", Type = typeof(IApplicationResult<bool>))]
        public async Task<IActionResult> CreateAsync([FromForm] CreateChannelCommand command) => await Mediator.Send(command);

    }
}
