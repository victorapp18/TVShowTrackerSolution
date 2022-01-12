using MediatR;
using Framework.Message.Concrete;

namespace TVShowTracker.Webapi.Application.Commands.Identity
{
    public class ExecutePasswordRetrieveCommand : ApplicationRequest, IRequest<ApplicationResult<bool>>
    {
        public string Token { get; set; }
        public string NewPassword { get; set; }
    }
}
