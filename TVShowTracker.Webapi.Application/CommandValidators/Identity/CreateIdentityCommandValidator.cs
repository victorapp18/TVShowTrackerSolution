using FluentValidation;
using TVShowTracker.Webapi.Application.Commands.Identity;

namespace TVShowTracker.Webapi.Application.CommandValidators.Identity
{
    public class CreateIdentityCommandValidator : AbstractValidator<CreateIdentityCommand>
    {
        public CreateIdentityCommandValidator() 
        {
            RuleFor(p => p.Name).NotEmpty()
                                .WithMessage("Name field cannot be empty.")
                                .NotNull()
                                .WithMessage("Name field cannot be null.");

            RuleFor(p => p.Username).NotEmpty()
                                     .WithMessage("Username field cannot be empty.")
                                     .NotNull()
                                     .WithMessage("Username field cannot be null.")
                                     .EmailAddress()
                                     .WithMessage("Invalid email address.");

            RuleFor(p => p.Contact).NotNull()
                                .WithMessage("Contact field is required.");

            RuleFor(p => p.RoleId).GreaterThan(0)
                                  .WithMessage("Role field is required.");
        }
    }
}

