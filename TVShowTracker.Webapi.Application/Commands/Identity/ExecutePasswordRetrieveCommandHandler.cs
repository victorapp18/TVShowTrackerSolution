using TVShowTracker.Webapi.Application.Services;
using Dm = TVShowTracker.Webapi.Domain.Aggregates.IdentityAggregate;
using MediatR;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Framework.Data.Abstractions.UnitOfWork;
using Framework.Message.Concrete;

namespace TVShowTracker.Webapi.Application.Commands.Identity
{
    public class ExecutePasswordRetrieveCommandHandler : IRequestHandler<ExecutePasswordRetrieveCommand, ApplicationResult<bool>>
    {
        private Dm.IIdentityRepository IdentityRepository { get; }
        private IUnitOfWork UnitOfWork { get; }

        public ExecutePasswordRetrieveCommandHandler(Dm.IIdentityRepository identityRepository,
                                                     IUnitOfWork unitOfWork) 
        {
            IdentityRepository = identityRepository;
            UnitOfWork = unitOfWork;
        }

        public async Task<ApplicationResult<bool>> Handle(ExecutePasswordRetrieveCommand request, CancellationToken cancellationToken)
        {
            ApplicationResult<bool> result = new ApplicationResult<bool>() { Message = "Invalid request, see validation field for more details." };
            request.HandleToken(request.Token);

            Dm.Identity identity = IdentityRepository.Get(request.IdentityId);

            if (identity == null) 
            {
                result.HttpStatusCode = HttpStatusCode.NotFound;
                result.Validations.Add("User not found.");
                return result;
            }

            if (!identity.IsActive) 
            {
                result.HttpStatusCode = HttpStatusCode.UnprocessableEntity;
                result.Validations.Add("Disabled user, contact system administrator.");
                return result;
            }

            Dm.PasswordRetrieve passwordRetrieve = identity.PasswordRetrieves
                .FirstOrDefault(it => request.Token == it.Token && !it.IsChanged);
                                                           
            if (passwordRetrieve == null) 
            {
                result.HttpStatusCode = HttpStatusCode.NotFound;
                result.Validations.Add("Any request for password retrieve could not be found. Please, try request it again.");
                return result;
            }

            if (passwordRetrieve.IsRetrievalExpired())
            {
                result.HttpStatusCode = HttpStatusCode.UnprocessableEntity;
                result.Validations.Add("Token has expired. Please, try request it again.");
                return result;
            }

            string password = PasswordService.GenerateHash(request.NewPassword, false);
            passwordRetrieve.SetPasswordChanged();
            
            identity.ChangePassword(password);
            identity.UpdatePasswordRetrieve(passwordRetrieve);

            IdentityRepository.Update(identity);
            await UnitOfWork.CommitAsync();

            result.Result = true;
            result.Message = "Your password has been changed successfully.";

            return result;
        }
    }
}
