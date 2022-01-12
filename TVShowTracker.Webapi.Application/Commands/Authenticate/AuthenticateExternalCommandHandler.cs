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
using System.Collections.Generic;
using System;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Identity;
using TVShowTracker.Webapi.Domain.DomainEvents;
using Framework.Seedworks.Concrete.Enums;
using Framework.Data.Abstractions.UnitOfWork;
using System.IO;
using FluentValidation;
using Newtonsoft.Json;
using TVShowTracker.Webapi.Domain.Aggregates.IdentityAggregate;
using System.Net.Http;

namespace TVShowTracker.Webapi.Application.Commands.Authenticate
{
    public class AuthenticateCommandHandler : IRequestHandler<AuthenticateExternalCommand, ApplicationResult<AuthenticateViewModel>>
    {
        private Dm.IIdentityRepository IdentityRepository { get; }
        private AppKeysOption KeyOptions { get; }
        private IMediator Mediator { get; }
        private IUnitOfWork UnitOfWork { get; }

        public AuthenticateCommandHandler(Dm.IIdentityRepository identityRepository,
                                          IOptions<AppKeysOption> keyOptions,
                                          IUnitOfWork unitOfWork,
                                          IMediator mediator)
        {
            IdentityRepository = identityRepository;
            KeyOptions = keyOptions.Value;
            Mediator = mediator;
            UnitOfWork = unitOfWork;
        }

        public async Task<ApplicationResult<AuthenticateViewModel>> Handle(AuthenticateExternalCommand request, CancellationToken cancellationToken)
        {
            ApplicationResult<AuthenticateViewModel> result = new ApplicationResult<AuthenticateViewModel>() { Message = "Invalid request, see validation field for more details." };

            TypeProvider provider = IdentityRepository.GetTypeProvider(request.Provider.ToUpper());
            if(provider == null){
                
                result.HttpStatusCode = HttpStatusCode.BadRequest;
                result.Validations.Add("Invalid Provider.");

                return result;
            }

            Dm.Identity identity = new Dm.Identity();
            ExternalLoginModel externalLoginModel = new ExternalLoginModel();

            if (request.Provider.ToUpper() == "GOOGLE")
            {
                var payload = await VerifyGoogleToken(request);
                if (payload == null)
                {
                    result.HttpStatusCode = HttpStatusCode.BadRequest;
                    result.Validations.Add("Invalid External Authentication.");

                    return result;
                }
                var info = new UserLoginInfo(request.Provider, payload.Subject, request.Provider);
                externalLoginModel.Id = info.ProviderKey;
                externalLoginModel.Email = payload.Email;
                externalLoginModel.Full_Name = payload.Name;
                externalLoginModel.Photo_Profile = payload.Picture;
            }
            
            if (request.Provider.ToUpper() == "FACEBOOK") {
                var payload = await VerifyFacebookToken(request);

                if (payload.Full_Name == null)
                {
                    result.HttpStatusCode = HttpStatusCode.BadRequest;
                    result.Validations.Add("Invalid External Authentication. Name null");

                    return result;
                }
                
                if (payload == null)
                {
                    result.HttpStatusCode = HttpStatusCode.BadRequest;
                    result.Validations.Add("Invalid External Authentication.");

                    return result;
                }
                externalLoginModel.Id = payload.Id;
                externalLoginModel.Email = payload.Email == null ? payload.Id : payload.Email;
                externalLoginModel.Full_Name = payload.Full_Name == null ? payload.First_Name + " " + payload.Last_Name : payload.Full_Name;
                externalLoginModel.Photo_Profile = payload.Photo_Profile;
            }
            

            identity = IdentityRepository.GetExternal(externalLoginModel.Email, externalLoginModel.Id, provider.TypeProviderId);
            if (identity == null)
            {

                Dm.Role role = IdentityRepository.GetRoleById(2);

                if (role == null)
                {
                    result.HttpStatusCode = HttpStatusCode.UnprocessableEntity;
                    result.Validations.Add("Role not found.");

                    return result;
                }

                identity = Dm.Identity.Create(externalLoginModel.Full_Name, externalLoginModel.Email, externalLoginModel.Id, role, true, null, null);
                IdentityRepository.Create(identity);

                Dm.Provider loginExternalProvider = Dm.Provider.Create(identity.IdentityId, provider.TypeProviderId);
                IdentityRepository.CreateProvider(loginExternalProvider);

                await UnitOfWork.CommitAsync(EventDispatchMode.AfterSaveChanges);

            }
            
            identity = Dm.Identity.SetPhotoExternal(externalLoginModel.Photo_Profile, identity);
            IdentityRepository.Create(identity);
            
            if (!identity.IsActive)
            {
                string message = "Disabled user, contact system administrator.";

                result.HttpStatusCode = HttpStatusCode.UnprocessableEntity;
                result.Validations.Add(message);

                /*await Mediator.Publish(new AccessHistoryAuthenticatedDomainEvent(identity, false, message));*/

                return result;
            }
            string token = TokenService.GetToken(identity, KeyOptions.jwt.clientSecret);
            result.Message = "Token has been created successfully.";
            result.Result = identity.Map(token);

            return result;
        }

        public async Task<GoogleJsonWebSignature.Payload> VerifyGoogleToken(AuthenticateExternalCommand externalAuth)
        {
            try
            {
                var payload = GoogleJsonWebSignature.ValidateAsync(externalAuth.IdToken, new GoogleJsonWebSignature.ValidationSettings()).Result; ;
                return payload;
            }
            catch (Exception ex)
            {
                //log an exception
                return null;
            }
        }

        public async Task<ExternalLoginModel> VerifyFacebookToken(AuthenticateExternalCommand externalAuth)
        {
            if (string.IsNullOrEmpty(externalAuth.IdToken))
            {
                throw new ValidationException("Invalid Facebook token");
            }

            ExternalLoginModel user = new ExternalLoginModel();
            var client = new HttpClient();

            var verifyTokenEndPoint = string.Format("https://graph.facebook.com/me?access_token={0}&fields=name,email,picture", externalAuth.IdToken);
            var verifyAppEndpoint = string.Format("https://graph.facebook.com/app?access_token={0}", externalAuth.IdToken);

            var uri = new Uri(verifyTokenEndPoint);
            var response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                dynamic userObj = (Newtonsoft.Json.Linq.JObject)Newtonsoft.Json.JsonConvert.DeserializeObject(content);

                uri = new Uri(verifyAppEndpoint);
                response = await client.GetAsync(uri);
                content = await response.Content.ReadAsStringAsync();
                dynamic appObj = (Newtonsoft.Json.Linq.JObject)Newtonsoft.Json.JsonConvert.DeserializeObject(content);

                //if (appObj["id"] == KeyOptions.facebook.clientId)
                //{
                    //token is from our App
                    user.Id = userObj["id"];
                    user.Email = userObj["email"];
                    user.Full_Name = userObj["name"];
                    dynamic photoObj = (Newtonsoft.Json.Linq.JObject)Newtonsoft.Json.JsonConvert.DeserializeObject(userObj["picture"].ToString());
                    user.Photo_Profile = photoObj.data.url;
                //}
                //return user;
            }
            return user;
        }
    }
}
