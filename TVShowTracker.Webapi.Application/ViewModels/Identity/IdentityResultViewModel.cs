using System;
using System.Runtime.Serialization;
namespace TVShowTracker.Webapi.Application.ViewModels.Identity
{
    [DataContract(Name = "identity", Namespace = "http://TVShowTracker.com/reuslt/type/identity")]
    public class IdentityResultViewModel
    {
        [DataMember(Name = "IdentityId")]
        public string IdentityId { get; set; }

        [DataMember(Name = "Name")]
        public string Name { get; set; }

        [DataMember(Name = "Username")]
        public string Username { get; set; }

        [DataMember(Name = "Password")]
        public string Password { get; set; }

        [DataMember(Name = "CreateDate")]
        public DateTime? CreateDate { get; set; }

        [DataMember(Name = "Contact")]
        public string Contact { get; set; }

        [DataMember(Name = "Image")]
        public string Image { get; set; }

        [DataMember(Name = "RoleDescription")]
        public string RoleDescription { get; set; }

        [DataMember(Name = "RoleName")]
        public string RoleName { get; set; }

        [DataMember(Name = "RoleId")]
        public int RoleId { get; set; }

        [DataMember(Name = "IsAccessExternal")]
        public bool IsAccessExternal { get; set; }

        [DataMember(Name = "ProviderId")]
        public int ProviderId { get; set; }

        [DataMember(Name = "ProviderName")]
        public String ProviderName { get; set; }
    }
}
