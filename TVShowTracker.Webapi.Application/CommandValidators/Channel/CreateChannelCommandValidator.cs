using FluentValidation;
using TVShowTracker.Webapi.Application.Commands.Channel;

namespace TVShowTracker.Webapi.Application.CommandValidators.Channel
{
    public class CreateChannelCommandValidator : AbstractValidator<CreateChannelCommand>
    {
        public CreateChannelCommandValidator() 
        {
           
            RuleFor(p => p.Name).NotEmpty()
                                     .WithMessage("Name field cannot be empty.")
                                     .NotNull()
                                     .WithMessage("Name field cannot be null.");

            RuleFor(p => p.Description).NotEmpty()
                                     .WithMessage("Description field cannot be empty.")
                                     .NotNull()
                                     .WithMessage("Description field cannot be null."); ;
            
            RuleFor(p => p.Namber).NotEmpty()
                               .WithMessage("Namber field cannot be empty.")
                               .NotNull()
                               .WithMessage("Namber field cannot be null.");
        }
    }
}

