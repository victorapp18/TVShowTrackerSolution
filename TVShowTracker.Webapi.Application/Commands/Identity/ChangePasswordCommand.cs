using MediatR;
using Framework.Message.Concrete;

namespace TVShowTracker.Webapi.Application.Commands.Identity
{
    public class ChangePasswordCommand : ApplicationRequest, IRequest<ApplicationResult<bool>>
    {
        public string NewPassword { get; set; }
    }
}
