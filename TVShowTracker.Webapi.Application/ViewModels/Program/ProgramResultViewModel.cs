using System;
using System.Runtime.Serialization;
namespace TVShowTracker.Webapi.Application.ViewModels.Program
{
    [DataContract(Name = "program", Namespace = "http://TVShowTracker.com/reuslt/type/program")]
    public class ProgramResultViewModel
    {
        [DataMember(Name = "ProgramId")]
        public string ProgramId { get; set; }

        [DataMember(Name = "ChannelId")]
        public string ChannelId { get; set; }

        [DataMember(Name = "GenderId")]
        public int GenderId { get; set; }

        [DataMember(Name = "GenderName")]
        public string GenderName { get; set; }

        [DataMember(Name = "Name")]
        public string Name { get; set; }

        [DataMember(Name = "Description")]
        public string Description { get; set; }

        [DataMember(Name = "ExhibitionDate")]
        public DateTime? ExhibitionDate { get; set; }

        [DataMember(Name = "CreateDate")]
        public DateTime? CreateDate { get; set; }

        [DataMember(Name = "UpdateDate")]
        public DateTime? UpdateDate { get; set; }
    }
}
