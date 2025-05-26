using FluentValidation;

namespace Queans.Application.Tags.Commands.UpdateTag
{
    public class UpdateTagCommandValidator : AbstractValidator<UpdateTagCommand>
    {
        public UpdateTagCommandValidator()
        {
            RuleFor(command => command.TagId)
                .NotNull().WithMessage("Tag ID must be not null");

            RuleFor(command => command.Name)
                .NotEmpty().WithMessage("Tag name must have at least one character");
        }
    }
}
