using System;
using Framework.Message.Concrete;

namespace TVShowTracker.Webapi.Application.ViewModels.Program
{
    public class ProgramRequestViewModel : ApplicationRequest
    {
        public Guid ChannelId { get; set; }
    }
}
