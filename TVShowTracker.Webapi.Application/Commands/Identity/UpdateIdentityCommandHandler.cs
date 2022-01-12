using TVShowTracker.Webapi.Application.Services;
using Dm = TVShowTracker.Webapi.Domain.Aggregates.IdentityAggregate;
using TVShowTracker.Webapi.Domain.DomainEvents;
using MediatR;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Framework.Data.Abstractions.UnitOfWork;
using Framework.Message.Concrete;
using Framework.Seedworks.Concrete.Enums;
using System.Linq;

namespace TVShowTracker.Webapi.Application.Commands.Identity
{
    public class UpdateIdentityCommandHandler : IRequestHandler<UpdateIdentityCommand, ApplicationResult<bool>>
    {
        private Dm.IIdentityRepository IdentityRepository { get; }
        private IUnitOfWork UnitOfWork { get; }

        public UpdateIdentityCommandHandler(Dm.IIdentityRepository identityRepository, 
                                            IUnitOfWork unitOfWork) 
        {
            IdentityRepository = identityRepository;
            UnitOfWork = unitOfWork;
        }

        public async Task<ApplicationResult<bool>> Handle(UpdateIdentityCommand request, CancellationToken cancellationToken)
        {
            ApplicationResult<bool> result = new ApplicationResult<bool>() { Result = false,  Message = "Invalid request, see validation field for more details." };

            Dm.Identity identity = IdentityRepository.Get(request.IdentityId);

            if (request.RoleId == 1)
            {
                result.HttpStatusCode = HttpStatusCode.UnprocessableEntity;
                result.Validations.Add("Profile unavailable. You do not have permission to change the profile from user to administrator, please choose another profile or contact the database administrator.");

                return result;
            }

            if (((request.Contact.Length > 14 || request.Contact.Length < 11) || containLetters(request.Contact)) && (request.Contact != null && request.Contact != ""))
            {
                result.HttpStatusCode = HttpStatusCode.UnprocessableEntity;
                result.Validations.Add("Invalid Contact.");

                return result;
            }
          
            request.Contact = request.Contact != null  ? request.Contact : identity.Contact;
            request.Name = request.Name != null ? request.Name : identity.Name;
            
            identity = identity.UpdateIdentity(request.Contact, request.Name, identity);
            
            Dm.IdentityRole identityRole = request.RoleId != null ? IdentityRepository.GetMyRoleByIdentityId(identity.IdentityId) : null;
            if(identityRole != null)
            {
                Dm.Role role = IdentityRepository.GetRoleById(request.RoleId);

                if (role == null)
                {
                    result.HttpStatusCode = HttpStatusCode.UnprocessableEntity;
                    result.Validations.Add("Role not found.");

                    return result;
                }

                identity = identity.RemoveIdentityRole(identity, identityRole);
                identity = identity.AddIdentityRole(role, identity);
            }

            IdentityRepository.Update(identity);
            await UnitOfWork.CommitAsync();

            result.Message = $"User has been update successfully.";
            result.Result = true;

            return result;
        }
        private bool containLetters(string text)
        {
            if (text.Where(c => char.IsLetter(c)).Count() > 0)
                return true;
            else
                return false;
        }

    }
}
