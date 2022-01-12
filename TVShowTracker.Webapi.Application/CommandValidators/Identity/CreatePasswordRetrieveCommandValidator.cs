using FluentValidation;
using TVShowTracker.Webapi.Application.Commands.Identity;

namespace TVShowTracker.Webapi.Application.CommandValidators.Identity
{
    public class CreatePasswordRetrieveCommandValidator : AbstractValidator<CreatePasswordRetrieveCommand>
    {
        public CreatePasswordRetrieveCommandValidator() 
        {
            RuleFor(p => p.Username).NotNull()
                                    .WithMessage("Username field cannot be null.");
        }
    }
}
