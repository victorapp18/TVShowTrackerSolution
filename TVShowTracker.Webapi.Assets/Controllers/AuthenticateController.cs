using TVShowTracker.Webapi.Application.Commands.Authenticate;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TVShowTracker.Webapi.Application.ViewModels.Authenticate;
using Framework.Message.Abstraction;

namespace TVShowTracker.WebApi.Assets.Controllers
{
    [ApiController]
    [Route("api/authenticate")]
    public class AuthenticateController : Controller
    {
        private IMediator Mediator { get; }

        public AuthenticateController(IMediator mediator) 
        {
            Mediator = mediator;
        }

        [HttpPost("internal"), Produces("application/json", Type = typeof(IApplicationResult<AuthenticateViewModel>))]
        public async Task<IActionResult> GetTokenInternalAsync([FromBody] AuthenticateInternalCommand command) => await Mediator.Send(command);

        [HttpPost("external"), Produces("application/json", Type = typeof(IApplicationResult<AuthenticateViewModel>))]
        public async Task<IActionResult> GetTokenExternalAsync([FromBody] AuthenticateExternalCommand command) => await Mediator.Send(command);
    }
}
