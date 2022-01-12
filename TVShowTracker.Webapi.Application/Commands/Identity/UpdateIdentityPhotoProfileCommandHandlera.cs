using Dm = TVShowTracker.Webapi.Domain.Aggregates.IdentityAggregate;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Framework.Data.Abstractions.UnitOfWork;
using Framework.Message.Concrete;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Drawing;
using System;
using Microsoft.AspNetCore.Hosting;
using System.Net;

namespace TVShowTracker.Webapi.Application.Commands.Identity
{
    public class UpdateIdentityPhotoProfileCommandHandler : IRequestHandler<UpdateIdentityPhotoProfileCommand, ApplicationResult<bool>>
    {
        private Dm.IIdentityRepository IdentityRepository { get; }
        private IUnitOfWork UnitOfWork { get; }
        public static IWebHostEnvironment _environment;
        public UpdateIdentityPhotoProfileCommandHandler(Dm.IIdentityRepository identityRepository, 
                                            IUnitOfWork unitOfWork, IWebHostEnvironment environment)
        {
            _environment = environment;
            IdentityRepository = identityRepository;
            UnitOfWork = unitOfWork;
        }

        public async Task<ApplicationResult<bool>> Handle(UpdateIdentityPhotoProfileCommand request, CancellationToken cancellationToken)
        {
            ApplicationResult<bool> result = new ApplicationResult<bool>() { Result = false,  Message = "Invalid request, see validation field for more details." };

            Dm.Identity identity = IdentityRepository.Get(request.IdentityId);

            string urlPhoto = await createPathUser(request.Username, request.Image);
            if (urlPhoto == null)
            {
                result.HttpStatusCode = HttpStatusCode.UnprocessableEntity;
                result.Validations.Add("Problem update profile picture on server.");

                return result;
            }

            identity = identity.UpdateIdentityPhotoProfile(urlPhoto, identity);

            IdentityRepository.Update(identity);
            await UnitOfWork.CommitAsync();

            result.Message = $"User profile picture has been successfully updated.";
            result.Result = true;

            return result;
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

            string urlPhoto = "http://34.216.221.213/photos/" + username + "/photo_profile.jpg";

            return urlPhoto;
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
