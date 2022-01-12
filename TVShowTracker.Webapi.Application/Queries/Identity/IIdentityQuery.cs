using TVShowTracker.Webapi.Application.ViewModels.Identity;
using TVShowTracker.Webapi.Application.ViewModels.Role;
using System.Collections.Generic;
using System.Threading.Tasks;
using Framework.Message.Abstraction;

namespace TVShowTracker.Webapi.Application.Queries.Identity
{
    public interface IIdentityQuery
    {
        Task<IApplicationResult<IEnumerable<IdentityResultViewModel>>> GetMyAsync(IdentityRequestViewModel request);
        Task<IApplicationResult<IEnumerable<RoleResultViewModel>>> GetRolesAsync();
    }
}
