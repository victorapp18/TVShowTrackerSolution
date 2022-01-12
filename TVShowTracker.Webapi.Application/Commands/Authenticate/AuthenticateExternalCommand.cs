using TVShowTracker.Webapi.Application.ViewModels.Authenticate;
using MediatR;
using Framework.Message.Concrete;

namespace TVShowTracker.Webapi.Application.Commands.Authenticate
{
    public class AuthenticateExternalCommand : IRequest<ApplicationResult<AuthenticateViewModel>>
    {
        public string Provider { get; set; }
        public string IdToken { get; set; }
    }
}
