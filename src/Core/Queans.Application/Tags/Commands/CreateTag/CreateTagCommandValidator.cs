using FluentValidation;

namespace Queans.Application.Tags.Commands.CreateTag
{
    public class CreateTagCommandValidator : AbstractValidator<CreateTagCommand>
    {
        public CreateTagCommandValidator()
        {
            RuleFor(command => command.Name)
                .NotEmpty().WithMessage("Tag name must have at least one character");
        }
    }
}
