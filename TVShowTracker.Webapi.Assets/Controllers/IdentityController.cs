using TVShowTracker.Webapi.Application.Commands.Identity;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Framework.Message.Abstraction;
using TVShowTracker.Webapi.Application.Queries.Identity;
using TVShowTracker.Webapi.Application.ViewModels.Identity;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using TVShowTracker.Webapi.Application.ViewModels.Role;

namespace TVShowTracker.WebApi.Assets.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/identities")]
    public class IdentityController : Controller
    {
        private IMediator Mediator { get; }
        private IIdentityQuery IdentityQuery { get; }

        public IdentityController(IMediator mediator, IIdentityQuery identityQuery) 
        {
            Mediator = mediator;
            IdentityQuery = identityQuery;
        }

        [HttpGet, Produces("application/json", Type = typeof(IApplicationResult<IEnumerable<IdentityResultViewModel>>))]
        public async Task<IActionResult> GetMyAsync([FromQuery] IdentityRequestViewModel request) => await IdentityQuery.GetMyAsync(request);

        [AllowAnonymous]
        [HttpPost, Produces("application/json", Type = typeof(IApplicationResult<bool>))]
        public async Task<IActionResult> CreateAsync([FromForm] CreateIdentityCommand command) => await Mediator.Send(command);

        [HttpPut, Produces("application/json", Type = typeof(IApplicationResult<bool>))]
        public async Task<IActionResult> ChangedAsync([FromBody] UpdateIdentityCommand command) => await Mediator.Send(command);
        
        [HttpPut("changePhotoProfile"), Produces("application/json", Type = typeof(IApplicationResult<bool>))]
        public async Task<IActionResult> ChangedPhotoProfileAsync([FromForm] UpdateIdentityPhotoProfileCommand command) => await Mediator.Send(command);

        [HttpPut("changePassword"), Produces("application/json", Type = typeof(IApplicationResult<bool>))]
        public async Task<IActionResult> ChangePasswordAsync([FromBody] ChangePasswordCommand command) => await Mediator.Send(command);

        [AllowAnonymous]
        [HttpPost("passwordRetrieve"), Produces("application/json", Type = typeof(IApplicationResult<bool>))]
        public async Task<IActionResult> CreatePasswordRetrieveAsync([FromBody] CreatePasswordRetrieveCommand command) => await Mediator.Send(command);

        [AllowAnonymous]
        [HttpPut("passwordRetrieve"), Produces("application/json", Type = typeof(IApplicationResult<bool>))]
        public async Task<IActionResult> ExecutePasswordRetrieveAsync([FromBody] ExecutePasswordRetrieveCommand command) => await Mediator.Send(command);

        [AllowAnonymous]
        [HttpGet("roles"), Produces("application/json", Type = typeof(IApplicationResult<IEnumerable<RoleResultViewModel>>))]
        public async Task<IActionResult> GetRolesAsync() => await IdentityQuery.GetRolesAsync();
    }
}
