using FluentValidation;
using TVShowTracker.Webapi.Application.Commands.Program;

namespace TVShowTracker.Webapi.Application.CommandValidators.Channel
{
    public class CreateProgramCommandValidator : AbstractValidator<CreateProgramCommand>
    {
        public CreateProgramCommandValidator() 
        {
           
            RuleFor(p => p.ChannelId).NotEmpty()
                                     .WithMessage("Channe field cannot be empty.")
                                     .NotNull()
                                     .WithMessage("Channe field cannot be null.");

            RuleFor(p => p.GenderId).NotEmpty()
                                     .WithMessage("Gender field cannot be empty.")
                                     .NotNull()
                                     .WithMessage("Gender field cannot be null."); ;
            
            RuleFor(p => p.Name).NotEmpty()
                               .WithMessage("Name field cannot be empty.")
                               .NotNull()
                               .WithMessage("Name field cannot be null.");

            RuleFor(p => p.Description).NotEmpty()
                               .WithMessage("Description field cannot be empty.")
                               .NotNull()
                               .WithMessage("Description field cannot be null.");

            RuleFor(p => p.ExhibitionDate).NotEmpty()
                               .WithMessage("ExhibitionDate field cannot be empty.")
                               .NotNull()
                               .WithMessage("ExhibitionDate field cannot be null.");
        }
    }
}

