using Framework.Seedworks.Domains.Abstraction;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace TVShowTracker.Webapi.Domain.Aggregates.IdentityAggregate
{
    public class Identity : Entity, IAggregateRoot
    {
        public Guid IdentityId { get; private set; }
        public string Name { get; private set; }
        public string Username { get; private set; }
        public DateTime CreateDate { get; private set; }
        public bool IsFirstAccess { get; private set; }
        public string Password { get; private set; }
        public string Contact { get; private set; }
        public DateTime UpdateDate { get; private set; }
        public bool IsActive { get; private set; }
        public bool IsAccessExternal { get; private set; }
        public string Image { get; set; }
        public ICollection<IdentityRole> IdentityRoles { get; private set; } = new List<IdentityRole>();
        public ICollection<AccessHistory> AccessHistories { get; private set; } = new List<AccessHistory>();
        public ICollection<PasswordRetrieve> PasswordRetrieves { get; private set; } = new List<PasswordRetrieve>();
        public ICollection<Provider> Providers { get; private set; } = new List<Provider>();
        public static Identity Create(string name, string username, string password, Role role, bool isAccessExternal,
                                        string contact, string image)
        {
            Identity identity = new Identity
            {
                IdentityId = Guid.NewGuid(),
                Name = name,
                Username = username,
                CreateDate = DateTime.Now,
                IsFirstAccess = true,
                Password = password,
                IsActive = true,
                IsAccessExternal = isAccessExternal,
                Contact = contact,
                Image = image
    };

            IdentityRole identityRole = IdentityRole.Create(role.RoleId);
            identity.IdentityRoles.Add(identityRole);

            return identity;
        }

        public void ChangePassword(string newPassword) 
        {
            Password = newPassword;
            UpdateDate = DateTime.Now;
            IsFirstAccess = false;
        }
        public void ChangeIsFirstAccess(bool isFirstAccess)
        {
            IsFirstAccess = isFirstAccess;
        }

        public void SetPasswordRetrieve(string token, string passwordProvisional) 
        {
            PasswordRetrieve passwordRetrieve = PasswordRetrieve.Create(IdentityId, token, passwordProvisional);
            PasswordRetrieves.Add(passwordRetrieve);
        }

        public void SetAccessHistory(AccessHistory accessHistory) => AccessHistories.Add(accessHistory);
        public void UpdatePasswordRetrieve(PasswordRetrieve passwordRetrieve)
        {
            PasswordRetrieves.Remove(passwordRetrieve);
            PasswordRetrieves.Add(passwordRetrieve);
        }

        public Identity UpdateIdentity(string contact, string name, Identity identity) 
        {
            identity.Name = name;
            identity.Contact = contact;
            
            return identity;
        }
        public Identity RemoveIdentityRole(Identity identity, IdentityRole identityRole)
        {
            identity.IdentityRoles.Remove(identityRole);

            return identity;
        }

        public Identity AddIdentityRole(Role role, Identity identity)
        {
            IdentityRole identityRole = IdentityRole.Create(role.RoleId);
            identity.IdentityRoles.Add(identityRole);

            return identity;
        }

        public Identity UpdateIdentityPhotoProfile(string image, Identity identity)
        {
            identity.Image = image;
            
            return identity;
        }
        public static Identity SetPhotoExternal(string image, Identity identity)
        {
            identity.Image = image;

            return identity;
        }
    }
}
