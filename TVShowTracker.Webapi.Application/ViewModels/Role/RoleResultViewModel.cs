using System;
using System.Runtime.Serialization;
namespace TVShowTracker.Webapi.Application.ViewModels.Role
{
    [DataContract(Name = "identity", Namespace = "http://TVShowTracker.com/reuslt/type/identity")]
    public class RoleResultViewModel
    {
        [DataMember(Name = "RoleDescription")]
        public string RoleDescription { get; set; }

        [DataMember(Name = "RoleName")]
        public string RoleName { get; set; }

        [DataMember(Name = "RoleId")]
        public int RoleId { get; set; }
    }
}
