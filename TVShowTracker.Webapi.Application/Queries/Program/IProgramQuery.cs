using System.Collections.Generic;
using System.Threading.Tasks;
using Framework.Message.Abstraction;
using TVShowTracker.Webapi.Application.ViewModels.Gender;
using TVShowTracker.Webapi.Application.ViewModels.Program;

namespace TVShowTracker.Webapi.Application.Queries.Program
{
    public interface IProgramQuery
    {
        Task<IApplicationResult<IEnumerable<ProgramResultViewModel>>> GetProgramAllAsync(ProgramRequestViewModel request);
        Task<IApplicationResult<IEnumerable<ProgramResultViewModel>>> GetProgramGendersAsync(ProgramGenderRequestViewModel request);
        Task<IApplicationResult<IEnumerable<GenderResultViewModel>>> GetGendersAsync();
    }
}
