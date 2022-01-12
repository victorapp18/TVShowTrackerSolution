using TVShowTracker.Webapi.Application.ViewModels.Channel;
using System.Collections.Generic;
using System.Threading.Tasks;
using Framework.Message.Abstraction;

namespace TVShowTracker.Webapi.Application.Queries.Channel
{
    public interface IChannelQuery
    {
        Task<IApplicationResult<IEnumerable<ChannelResultViewModel>>> GetAllAsync();
    }
}
