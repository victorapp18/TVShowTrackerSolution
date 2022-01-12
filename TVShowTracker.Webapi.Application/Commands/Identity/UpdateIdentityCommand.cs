using MediatR;
using Framework.Message.Concrete;

namespace TVShowTracker.Webapi.Application.Commands.Identity
{
    public class UpdateIdentityCommand : ApplicationRequest, IRequest<ApplicationResult<bool>>
    {
        public UpdateIdentityCommand() { }

        //new public string Password { get; set; }
        public string Name { get; set; }
        public string Contact { get; set; }
        public int RoleId { get; set; }

    }
}
