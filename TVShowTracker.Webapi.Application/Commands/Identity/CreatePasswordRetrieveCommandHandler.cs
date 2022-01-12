using TVShowTracker.Webapi.Application.Options;
using TVShowTracker.Webapi.Application.Services;
using Dm = TVShowTracker.Webapi.Domain.Aggregates.IdentityAggregate;
using TVShowTracker.Webapi.Domain.DomainEvents;
using MediatR;
using Microsoft.Extensions.Options;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Framework.Data.Abstractions.UnitOfWork;
using Framework.Seedworks.Concrete.Enums;
using Framework.Message.Concrete;

namespace TVShowTracker.Webapi.Application.Commands.Identity
{
    public class CreatePasswordRetrieveCommandHandler : IRequestHandler<CreatePasswordRetrieveCommand, ApplicationResult<bool>>
    {
        private Dm.IIdentityRepository IdentityRepository { get; }
        private IUnitOfWork UnitOfWork { get; }
        private AppKeysOption AppKeysOption { get; }

        public CreatePasswordRetrieveCommandHandler(Dm.IIdentityRepository identityRepository,
                                                    IUnitOfWork unitOfWork,
                                                    IOptions<AppKeysOption> appKeysOption) 
        {
            IdentityRepository = identityRepository;
            UnitOfWork = unitOfWork;
            AppKeysOption = appKeysOption.Value;
        }

        public async Task<ApplicationResult<bool>> Handle(CreatePasswordRetrieveCommand request, CancellationToken cancellationToken)
        {
            ApplicationResult<bool> result = new ApplicationResult<bool>() { Message = "Invalid request, see validation field for more details." };
            Dm.Identity identity = IdentityRepository.Get(request.Username);

            if (identity == null)
            {
                result.HttpStatusCode = HttpStatusCode.NotFound;
                result.Validations.Add("User not found.");
                return result;
            }

            if (!identity.IsActive)
            {
                result.HttpStatusCode = HttpStatusCode.UnprocessableEntity;
                result.Validations.Add("Disable user, contact system administrator.");
                return result;
            }

            string passwordProvisional = PasswordService.GeneratePassword();
            string token = TokenService.GetToken(identity, AppKeysOption.jwt.clientSecret);
            identity.SetPasswordRetrieve(token, PasswordService.GenerateHash(passwordProvisional, false));
            identity.AddDomainEvent(new PasswordRetrieveCreatetedDomainEvent(identity, passwordProvisional, request.Language));

            IdentityRepository.Update(identity);
            await UnitOfWork.CommitAsync(EventDispatchMode.AfterSaveChanges);

            result.Result = true;
            result.Message = "An email was sent to your address with instructions to reset your password.";

            return result;
        }
    }
}
