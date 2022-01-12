using FluentValidation;
using TVShowTracker.Webapi.Application.Commands.Identity;

namespace TVShowTracker.Webapi.Application.CommandValidators.Identity
{
    public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
    {
        public ChangePasswordCommandValidator() 
        {
            RuleFor(p => p.NewPassword).NotNull()
                                       .WithMessage("Password field cannot be null.");
        }
    }
}
