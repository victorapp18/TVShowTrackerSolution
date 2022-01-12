using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Framework.Message.Abstraction;
using System.Collections.Generic;
using TVShowTracker.Webapi.Application.ViewModels.Gender;
using TVShowTracker.Webapi.Application.ViewModels.Program;
using TVShowTracker.Webapi.Application.Commands.Program;
using TVShowTracker.Webapi.Application.Queries.Program;

namespace TVShowTracker.WebApi.Assets.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/programs")]
    public class ProgramController : Controller
    {
        private IMediator Mediator { get; }
        private IProgramQuery ProgramQuery { get; }

        public ProgramController(IMediator mediator, IProgramQuery programQuery) 
        {
            Mediator = mediator;
            ProgramQuery = programQuery;
        }

        [HttpGet, Produces("application/json", Type = typeof(IApplicationResult<IEnumerable<ProgramResultViewModel>>))]
        public async Task<IActionResult> GetProgramAllAsync([FromForm] ProgramRequestViewModel request) => await ProgramQuery.GetProgramAllAsync(request);

        [HttpPost, Produces("application/json", Type = typeof(IApplicationResult<bool>))]
        public async Task<IActionResult> CreateAsync([FromForm] CreateProgramCommand command) => await Mediator.Send(command);
        
        [AllowAnonymous]
        [HttpGet("genders"), Produces("application/json", Type = typeof(IApplicationResult<IEnumerable<GenderResultViewModel>>))]
        public async Task<IActionResult> GetGendersAsync() => await ProgramQuery.GetGendersAsync();

        [HttpGet("programGenders"), Produces("application/json", Type = typeof(IApplicationResult<IEnumerable<ProgramResultViewModel>>))]
        public async Task<IActionResult> GetProgramGendersAsync([FromForm] ProgramGenderRequestViewModel request) => await ProgramQuery.GetProgramGendersAsync(request);

    }
}
