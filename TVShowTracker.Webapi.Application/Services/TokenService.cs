using TVShowTracker.Webapi.Domain.Aggregates.IdentityAggregate;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace TVShowTracker.Webapi.Application.Services
{
    internal static class TokenService
    {
        internal static string GetToken(Identity identity, string tokenSecret) 
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(tokenSecret);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(GetClaims(identity).ToArray()),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private static List<Claim> GetClaims(Identity identity) 
        {
            List<Claim> claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.PrimarySid, identity.IdentityId.ToString()));
            claims.Add(new Claim(ClaimTypes.Name, identity.Name));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, identity.Username));
              if (!identity.IsAccessExternal)
            {
                claims.Add(new Claim("contact", identity.Contact));
            }
            else
            {
                identity.Providers.ToList().ForEach(it => claims.Add(new Claim("provider", it.TypeProvider.Name)));
                identity.Providers.ToList().ForEach(it => claims.Add(new Claim("idprovider", it.TypeProvider.TypeProviderId.ToString())));
            }
            claims.Add(new Claim("isfirstaccess", identity.IsFirstAccess.ToString().ToLower(), "boolean"));
            claims.Add(new Claim("isaccessexternal", identity.IsAccessExternal.ToString().ToLower(), "boolean"));
            if (identity.Image != null)
            {
                claims.Add(new Claim("photoprofile", identity.Image));
            }
            identity.IdentityRoles.ToList().ForEach(it => claims.Add(new Claim(ClaimTypes.Role, it.Role.Name)));

            return claims;
        }
    }
}
