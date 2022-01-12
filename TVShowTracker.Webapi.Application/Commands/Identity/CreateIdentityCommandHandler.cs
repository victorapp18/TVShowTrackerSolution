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
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.Drawing;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace TVShowTracker.Webapi.Application.Commands.Identity
{
    public class CreateIdentityCommandHandler : IRequestHandler<CreateIdentityCommand, ApplicationResult<bool>>
    {
        private Dm.IIdentityRepository IdentityRepository { get; }
        private IUnitOfWork UnitOfWork { get; }
        public static IWebHostEnvironment _environment;
        
        public static IConfiguration Configuration;

        public CreateIdentityCommandHandler(Dm.IIdentityRepository identityRepository, 
                                            IUnitOfWork unitOfWork, IWebHostEnvironment environment,
                                            IConfiguration configuration)
        {
            _environment = environment;
            IdentityRepository = identityRepository;
            UnitOfWork = unitOfWork;
            Configuration = configuration;
        }

        public async Task<ApplicationResult<bool>> Handle(CreateIdentityCommand request, CancellationToken cancellationToken)
        {
            ApplicationResult<bool> result = new ApplicationResult<bool>() { Result = false,  Message = "Invalid request, see validation field for more details." };

            Dm.Identity identity = IdentityRepository.Get(request.Username);

            if ((request.Contact.Length > 14 || request.Contact.Length < 11) || containLetters(request.Contact))
            {
                result.HttpStatusCode = HttpStatusCode.UnprocessableEntity;
                result.Validations.Add("Invalid Contact.");

                return result;
            }
           
            if (identity != null)
            {
                result.HttpStatusCode = HttpStatusCode.UnprocessableEntity;
                result.Validations.Add("Username already exists.");

                return result;
            }

            Dm.Role role = IdentityRepository.GetRoleById(request.RoleId);

            if (role == null)
            {
                result.HttpStatusCode = HttpStatusCode.UnprocessableEntity;
                result.Validations.Add("Role not found.");

                return result;
            }

            string password = PasswordService.GeneratePassword();

            string urlPhoto = null;
            if (request.Image != null)
            {
                //urlPhoto = await createPathUser(request.Username, request.Image);
                //if (urlPhoto == null)
                //{
                //    result.HttpStatusCode = HttpStatusCode.UnprocessableEntity;
                //    result.Validations.Add("Problem inserting image on server.");

                //    return result;
                //}
            }

            identity = Dm.Identity.Create(request.Name, request.Username, PasswordService.GenerateHash(password, false), role, false, request.Contact, urlPhoto);
            identity.AddDomainEvent(new IdentityCreatedDomainEvent(identity, password, request.Language));

            IdentityRepository.Create(identity);
            await UnitOfWork.CommitAsync(EventDispatchMode.AfterSaveChanges);

            result.Message = $"User has been created successfully.";
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

        private async Task<string> createPathUser(string username, IFormFile file)
        {
            string path = Path.Combine(_environment.ContentRootPath, "photos");
            string pathUser = path + "\\" + username;
            if (!Directory.Exists(pathUser))
            {
                Directory.CreateDirectory(pathUser);
            }

            await saveImage(pathUser, file);
            
            string urlPhoto = UrlConnection() + username + "/photo_profile.jpg";

            return urlPhoto;
        }

        private string UrlConnection()
        {
            string connection = Configuration.GetConnectionString("UrlConnection");

            return connection;
        }

        private async Task saveImage(string path, IFormFile file)
        {
            Stream stream = file.OpenReadStream();

            using (var memoryStream = new MemoryStream())
            {
                Image imagemBitmap = Image.FromStream(stream);
                Size newSize = new Size(640, 640); //49,5 KB
                Bitmap bitmap = new Bitmap(imagemBitmap, newSize);

                bitmap.Save(path + "\\photo_profile.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);   
            }
        }
    }
}
