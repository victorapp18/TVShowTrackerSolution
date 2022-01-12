using FluentValidation;
using TVShowTracker.Webapi.Application.Commands.Authenticate;

namespace TVShowTracker.Webapi.Application.CommandValidators.Authenticate
{
    public class AuthenticateInternalCommandValidator : AbstractValidator<AuthenticateInternalCommand>
    {
        public AuthenticateInternalCommandValidator() 
        {
            RuleFor(p => p.Username).NotNull()
                                    .WithMessage("Username field cannot be null.")
                                    .EmailAddress()
                                    .WithMessage("Invalid email address.");

            RuleFor(p => p.Password).NotNull()
                                    .WithMessage("Password field cannot be null.");
        }
    }
}
