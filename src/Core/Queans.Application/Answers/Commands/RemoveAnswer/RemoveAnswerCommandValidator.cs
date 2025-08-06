using FluentValidation;

namespace Queans.Application.Answers.Commands.RemoveAnswer
{
    public class RemoveAnswerCommandValidator : AbstractValidator<RemoveAnswerCommand>
    {
        public RemoveAnswerCommandValidator()
        {
            RuleFor(command => command.AnswerId)
                .NotEmpty().WithMessage("Incorrect answer identifier");
        }
    }
}
