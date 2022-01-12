using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using TVShowTracker.Webapi.Application.Services;
using TVShowTracker.Webapi.Application.ViewModels.Identity;
using TVShowTracker.Webapi.Application.ViewModels.Role;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Framework.Message.Abstraction;
using Framework.Message.Concrete;
using Dm = TVShowTracker.Webapi.Domain.Aggregates.IdentityAggregate;

namespace TVShowTracker.Webapi.Application.Queries.Identity
{
    public class IdentityQuery : IIdentityQuery
    {
        private IConfiguration Configuration { get; }

        private Dm.IIdentityRepository IdentityRepository { get; }

        public IdentityQuery(Dm.IIdentityRepository identityRepository, IConfiguration configuration)
        {
            Configuration = configuration;
            IdentityRepository = identityRepository;
        }

        public async Task<IApplicationResult<IEnumerable<IdentityResultViewModel>>> GetMyAsync(IdentityRequestViewModel request)
        {
            IApplicationResult<IEnumerable<IdentityResultViewModel>> result = new ApplicationResult<IEnumerable<IdentityResultViewModel>>();

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("IdentityId", request.IdentityId.ToString());
            parameters.Add("Username", request.Username);

            Dm.Identity identity = IdentityRepository.Get(request.IdentityId);


            if (!identity.IsAccessExternal)
            {
                using (MySqlConnection connection = ConnectorService.GetMySqlConnection(Configuration))
                {
                    result.Result = connection.QueryMultiple(RawSql.GetIdentity, parameters).Read<IdentityResultViewModel>()
                                         .ToList();
                }
            }
            else
            {
                using (MySqlConnection connection = ConnectorService.GetMySqlConnection(Configuration))
                {
                    result.Result = connection.QueryMultiple(RawSql.GetProviderIdentity, parameters).Read<IdentityResultViewModel>()
                                         .ToList();
                }
            }
            
            if (result.Result.Count() == 0)
            {
                result.HttpStatusCode = HttpStatusCode.UnprocessableEntity;
                result.Validations.Add("Invalid identity. request can only return logged user record.");

                return result;
            }

            return result;
        }

        public async Task<IApplicationResult<IEnumerable<RoleResultViewModel>>> GetRolesAsync()
        {
            IApplicationResult<IEnumerable<RoleResultViewModel>> result = new ApplicationResult<IEnumerable<RoleResultViewModel>>();

            DynamicParameters parameters = new DynamicParameters();

            using (MySqlConnection connection = ConnectorService.GetMySqlConnection(Configuration))
            {
                result.Result = connection.QueryMultiple(RawSql.GetRoles, parameters).Read<RoleResultViewModel>()
                                     .ToList();
            }

            if (result.Result.Count() == 0)
            {
                result.HttpStatusCode = HttpStatusCode.UnprocessableEntity;
                result.Validations.Add("Error when searching profile. Search returned 0 record");

                return result;
            }

            return result;
        }

    }
}
