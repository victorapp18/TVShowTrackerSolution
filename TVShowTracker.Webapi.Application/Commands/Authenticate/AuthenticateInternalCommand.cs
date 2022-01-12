using TVShowTracker.Webapi.Application.ViewModels.Authenticate;
using MediatR;
using Framework.Message.Concrete;

namespace TVShowTracker.Webapi.Application.Commands.Authenticate
{
    public class AuthenticateInternalCommand : IRequest<ApplicationResult<AuthenticateViewModel>>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
