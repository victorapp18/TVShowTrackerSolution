using MediatR;
using Microsoft.AspNetCore.Http;
using Framework.Message.Concrete;

namespace TVShowTracker.Webapi.Application.Commands.Identity
{
    public class UpdateIdentityPhotoProfileCommand : ApplicationRequest, IRequest<ApplicationResult<bool>>
    {
        public UpdateIdentityPhotoProfileCommand() { }

        public IFormFile Image { get; set; }
    }
}
