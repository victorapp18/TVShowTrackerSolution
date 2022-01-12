using TVShowTracker.Webapi.Application.ViewModels.Authenticate;
using TVShowTracker.Webapi.Domain.Aggregates.IdentityAggregate;
using System;

namespace TVShowTracker.Webapi.Application.Mappers
{
    public static class IdentityMapper
    {
        internal static AuthenticateViewModel Map(this Identity identity, string token) 
        {
            return new AuthenticateViewModel
            {
                Token = token,
                Expires = DateTime.Now.AddHours(2)
            };
        }
    }
}
