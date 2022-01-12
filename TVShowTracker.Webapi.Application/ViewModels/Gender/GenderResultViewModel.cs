using System;
using System.Runtime.Serialization;
namespace TVShowTracker.Webapi.Application.ViewModels.Gender
{
    [DataContract(Name = "gender", Namespace = "http://TVShowTracker.com/reuslt/type/gender")]
    public class GenderResultViewModel
    {
        [DataMember(Name = "GenderId")]
        public string GenderId { get; set; }

        [DataMember(Name = "Name")]
        public string Name { get; set; }

        [DataMember(Name = "Description")]
        public string Description { get; set; }
    }
}
