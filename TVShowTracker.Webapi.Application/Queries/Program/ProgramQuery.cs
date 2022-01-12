using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using TVShowTracker.Webapi.Application.Services;
using TVShowTracker.Webapi.Application.ViewModels.Program;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Framework.Message.Abstraction;
using Framework.Message.Concrete;
using TVShowTracker.Webapi.Application.ViewModels.Gender;

namespace TVShowTracker.Webapi.Application.Queries.Program
{
    public class ProgramQuery : IProgramQuery
    {
        private IConfiguration Configuration { get; }

        public ProgramQuery(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public async Task<IApplicationResult<IEnumerable<ProgramResultViewModel>>> GetProgramAllAsync(ProgramRequestViewModel request)
        {
            IApplicationResult<IEnumerable<ProgramResultViewModel>> result = new ApplicationResult<IEnumerable<ProgramResultViewModel>>();
           
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("ChannelId", request.ChannelId.ToString());

            using (MySqlConnection connection = ConnectorService.GetMySqlConnection(Configuration))
            {
                    result.Result = connection.QueryMultiple(RawSql.GetProgramAll, parameters).Read<ProgramResultViewModel>()
                                         .ToList();
            }
                        
            if (result.Result.Count() == 0)
            {
                result.HttpStatusCode = HttpStatusCode.UnprocessableEntity;
                result.Validations.Add("Invalid Program. request can only return logged user record.");

                return result;
            }

            return result;
        }

        public async Task<IApplicationResult<IEnumerable<GenderResultViewModel>>> GetGendersAsync()
        {
            IApplicationResult<IEnumerable<GenderResultViewModel>> result = new ApplicationResult<IEnumerable<GenderResultViewModel>>();

            DynamicParameters parameters = new DynamicParameters();

            using (MySqlConnection connection = ConnectorService.GetMySqlConnection(Configuration))
            {
                result.Result = connection.QueryMultiple(RawSql.GetGenders, parameters).Read<GenderResultViewModel>()
                                     .ToList();
            }

            if (result.Result.Count() == 0)
            {
                result.HttpStatusCode = HttpStatusCode.UnprocessableEntity;
                result.Validations.Add("Error when searching gender. Search returned 0 record");

                return result;
            }

            return result;
        }
        public async Task<IApplicationResult<IEnumerable<ProgramResultViewModel>>> GetProgramGendersAsync(ProgramGenderRequestViewModel request)
        {
            IApplicationResult<IEnumerable<ProgramResultViewModel>> result = new ApplicationResult<IEnumerable<ProgramResultViewModel>>();

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("GenderId", request.GenderId);

            using (MySqlConnection connection = ConnectorService.GetMySqlConnection(Configuration))
            {
                result.Result = connection.QueryMultiple(RawSql.GetProgramGenderAll, parameters).Read<ProgramResultViewModel>()
                                     .ToList();
            }

            if (result.Result.Count() == 0)
            {
                result.HttpStatusCode = HttpStatusCode.UnprocessableEntity;
                result.Validations.Add("Invalid Program. request can only return logged user record.");

                return result;
            }

            return result;
        }
    }
}
