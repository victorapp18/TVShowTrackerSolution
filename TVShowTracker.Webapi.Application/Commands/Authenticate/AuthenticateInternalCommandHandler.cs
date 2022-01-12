using TVShowTracker.Webapi.Application.Mappers;
using TVShowTracker.Webapi.Application.Options;
using TVShowTracker.Webapi.Application.Services;
using TVShowTracker.Webapi.Application.ViewModels.Authenticate;
using MediatR;
using Microsoft.Extensions.Options;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Dm = TVShowTracker.Webapi.Domain.Aggregates.IdentityAggregate;
using Framework.Message.Concrete;
using Framework.Data.Abstractions.UnitOfWork;
using Framework.Seedworks.Concrete.Enums;
using System;

namespace TVShowTracker.Webapi.Application.Commands.Authenticate
{
    public class AuthenticateInternalCommandHandler : IRequestHandler<AuthenticateInternalCommand, ApplicationResult<AuthenticateViewModel>>
    {
        private Dm.IIdentityRepository IdentityRepository { get; }
        private AppKeysOption KeyOptions { get; }
        private IUnitOfWork UnitOfWork { get; }
        private IMediator Mediator { get; }

        public AuthenticateInternalCommandHandler(Dm.IIdentityRepository identityRepository,
                                          IUnitOfWork unitOfWork,
                                          IOptions<AppKeysOption> keyOptions,
                                          IMediator mediator) 
        {
            IdentityRepository = identityRepository;
            KeyOptions = keyOptions.Value;
            UnitOfWork = unitOfWork;
            Mediator = mediator;
        }

        public async Task<ApplicationResult<AuthenticateViewModel>> Handle(AuthenticateInternalCommand request, CancellationToken cancellationToken)
        {
            ApplicationResult<AuthenticateViewModel> result = new ApplicationResult<AuthenticateViewModel>() { Message = "Invalid request, see validation field for more details." };

            string password = PasswordService.GenerateHash(request.Password, false);
            Dm.Identity identity = IdentityRepository.GetInternal(request.Username, password);

            if (identity == null)
            {
                Dm.PasswordRetrieve validTokenProvisional = IdentityRepository.GetPasswordProvisional(request.Username, password);
                if (validTokenProvisional != null)
                {
                    if (validTokenProvisional.IsRetrievalExpired())
                    {
                        result.HttpStatusCode = HttpStatusCode.UnprocessableEntity;
                        result.Validations.Add("Token has expired. Please, try request it again.");
                        return result;
                    }
                    else
                    {
                        identity = IdentityRepository.Get(validTokenProvisional.IdentityId);
                        identity.ChangeIsFirstAccess(true);

                        validTokenProvisional.SetPasswordChanged();
                        identity.UpdatePasswordRetrieve(validTokenProvisional);
                    }
                } else {
                    result.HttpStatusCode = HttpStatusCode.NotFound;
                    result.Validations.Add("User not found.");

                    return result;
                }
            }

            if (!identity.IsActive)
            {
                string message = "Disabled user, contact system administrator.";

                result.HttpStatusCode = HttpStatusCode.UnprocessableEntity;
                result.Validations.Add(message);

                /*await Mediator.Publish(new AccessHistoryAuthenticatedDomainEvent(identity, false, message));*/

                return result;
            }

            string token = TokenService.GetToken(identity, KeyOptions.jwt.clientSecret);
            if (identity.IsFirstAccess)
            {
                identity.SetPasswordRetrieve(token, PasswordService.GenerateHash(password, false));
                IdentityRepository.Update(identity);
            }
            await UnitOfWork.CommitAsync(EventDispatchMode.AfterSaveChanges);

            result.Message = "Token has been created successfully.";
            result.Result = identity.Map(token);

            return result;
        }
    }
}
