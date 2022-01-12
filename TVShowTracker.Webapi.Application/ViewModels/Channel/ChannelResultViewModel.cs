using System;
using System.Runtime.Serialization;
namespace TVShowTracker.Webapi.Application.ViewModels.Channel
{
    [DataContract(Name = "channel", Namespace = "http://TVShowTracker.com/reuslt/type/channel")]
    public class ChannelResultViewModel
    {
        [DataMember(Name = "ChannelId")]
        public string ChannelId { get; set; }

        [DataMember(Name = "Name")]
        public string Name { get; set; }

        [DataMember(Name = "Namber")]
        public int Namber { get; set; }

        [DataMember(Name = "Description")]
        public string Description { get; set; }

        [DataMember(Name = "CreateDate")]
        public DateTime? CreateDate { get; set; }

        [DataMember(Name = "UpdateDate")]
        public DateTime? UpdateDate { get; set; }

    }
}
