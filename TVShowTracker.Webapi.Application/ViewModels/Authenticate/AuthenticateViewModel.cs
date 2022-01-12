using System;
using System.Runtime.Serialization;

namespace TVShowTracker.Webapi.Application.ViewModels.Authenticate
{
    [DataContract(Name = "authenticate", Namespace = "http://TVShowTracker.com/reuslt/type")]
    public class AuthenticateViewModel
    {
        [DataMember(Name = "token")]
        public string Token { get; set; }

        [DataMember(Name = "expires")]
        public DateTime Expires { get; set; }
    }
}
