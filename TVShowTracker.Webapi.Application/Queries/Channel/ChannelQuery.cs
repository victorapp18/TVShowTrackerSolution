using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using TVShowTracker.Webapi.Application.Services;
using TVShowTracker.Webapi.Application.ViewModels.Channel;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Framework.Message.Abstraction;
using Framework.Message.Concrete;
using Dm = TVShowTracker.Webapi.Domain.Aggregates.ChannelAggregate;

namespace TVShowTracker.Webapi.Application.Queries.Channel
{
    public class ChannelQuery : IChannelQuery
    {
        private IConfiguration Configuration { get; }

        private Dm.IChannelRepository ChannelRepository { get; }

        public ChannelQuery(Dm.IChannelRepository channelRepository, IConfiguration configuration)
        {
            Configuration = configuration;
            ChannelRepository = channelRepository;
        }

        public async Task<IApplicationResult<IEnumerable<ChannelResultViewModel>>> GetAllAsync()
        {
            IApplicationResult<IEnumerable<ChannelResultViewModel>> result = new ApplicationResult<IEnumerable<ChannelResultViewModel>>();

            using (MySqlConnection connection = ConnectorService.GetMySqlConnection(Configuration))
            {
                    result.Result = connection.QueryMultiple(RawSql.GetAll, null).Read<ChannelResultViewModel>()
                                         .ToList();
            }
                        
            if (result.Result.Count() == 0)
            {
                result.HttpStatusCode = HttpStatusCode.UnprocessableEntity;
                result.Validations.Add("Invalid channel. request can only return logged user record.");

                return result;
            }

            return result;
        }
    }
}
