using FluentValidation;
using TVShowTracker.Webapi.Application.Commands.Authenticate;

namespace TVShowTracker.Webapi.Application.CommandValidators.Authenticate
{
    public class AuthenticateExternalCommandValidator : AbstractValidator<AuthenticateExternalCommand>
    {
        public AuthenticateExternalCommandValidator() 
        {
            RuleFor(p => p.IdToken).NotNull()
                                    .WithMessage("IdToken field cannot be null.");

            RuleFor(p => p.Provider).NotNull()
                                    .WithMessage("Provider field cannot be null.");
        }
    }
}
