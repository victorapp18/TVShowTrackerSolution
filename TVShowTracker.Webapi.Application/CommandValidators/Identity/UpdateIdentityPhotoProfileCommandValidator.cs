using FluentValidation;
using TVShowTracker.Webapi.Application.Commands.Identity;

namespace TVShowTracker.Webapi.Application.CommandValidators.Identity
{
    public class UpdateIdentityPhotoProfileCommandValidator : AbstractValidator<UpdateIdentityPhotoProfileCommand>
    {
        public UpdateIdentityPhotoProfileCommandValidator() 
        {
            RuleFor(p => p.Image).NotNull()
                                    .WithMessage("Photo profile field cannot be null.");
        }
    }
}
