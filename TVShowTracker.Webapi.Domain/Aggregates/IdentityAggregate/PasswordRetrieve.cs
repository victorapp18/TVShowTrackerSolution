using System;
using Framework.Seedworks.Domains.Abstraction;

namespace TVShowTracker.Webapi.Domain.Aggregates.IdentityAggregate
{
    public class PasswordRetrieve : Entity
    {
        public Guid PasswordRetrieveId { get; private set; }
        public Guid IdentityId { get; private set; }
        public string Token { get; private set; }
        public DateTime CreateDate { get; private set; }
        public DateTime ExpirationDate { get; private set; }
        public DateTime? UpdateDate { get; private set; }
        public bool IsChanged { get; private set; }

        public Identity Identity { get; private set; }
        public string PasswordProvisional { get; private set; }

        public static PasswordRetrieve Create(Guid identityId, string token, string passwordProvisional) 
        {
            return new PasswordRetrieve 
            {
                IdentityId = identityId,
                Token = token,
                CreateDate = DateTime.Now,
                ExpirationDate = DateTime.Now.AddDays(1),
                IsChanged = false,
                PasswordProvisional = passwordProvisional
            };
        }

        public bool IsRetrievalExpired() => (ExpirationDate <= DateTime.Now);

        public void SetPasswordChanged()
        {
            IsChanged = true;
            UpdateDate = DateTime.Now;
        }

        public void ChangePasswordProvisional(string passwordProvisional)
        {
            PasswordProvisional = passwordProvisional;
        }
    }
}
