using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using Framework.Message.Concrete;

namespace TVShowTracker.Webapi.Application.Commands.Identity
{
    public class CreateIdentityCommand :  IRequest<ApplicationResult<bool>>
    {
        public CreateIdentityCommand() { }

        new public string Username { get; set; }
        //new public string Password { get; set; }
        public string Name { get; set; }
        public int RoleId { get; set; }
        public string Contact { get; set; }
        public string Language { get; set; }
        public IFormFile Image { get; set; }
    }
}
