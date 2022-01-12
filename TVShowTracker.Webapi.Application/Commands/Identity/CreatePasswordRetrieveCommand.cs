using MediatR;
using Framework.Message.Concrete;

namespace TVShowTracker.Webapi.Application.Commands.Identity
{
    public class CreatePasswordRetrieveCommand : IRequest<ApplicationResult<bool>>
    {
        public string Username { get; set; }
        public string Language { get; set; }
    }
}
