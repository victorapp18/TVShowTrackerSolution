using FluentValidation;
using TVShowTracker.Webapi.Application.Commands.Identity;

namespace TVShowTracker.Webapi.Application.CommandValidators.Identity
{
    public class ExecutePasswordRetrieveCommandValidator : AbstractValidator<ExecutePasswordRetrieveCommand>
    {
        public ExecutePasswordRetrieveCommandValidator() 
        {
            RuleFor(p => p.Token).NotNull()
                                 .WithMessage("Token field cannot be null.");

            RuleFor(p => p.NewPassword).NotNull()
                                       .WithMessage("Newpassword field cannot be null.");
        }
    }
}
