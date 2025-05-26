using FluentValidation;

namespace Queans.Application.Tags.Commands.DeleteTag
{
    public class DeleteTagCommandValidator : AbstractValidator<DeleteTagCommand>
    {
        public DeleteTagCommandValidator()
        {
            RuleFor(command => command.Id)
                .NotEmpty().WithMessage("Tag ID must be not empty");
        }
    }
}
