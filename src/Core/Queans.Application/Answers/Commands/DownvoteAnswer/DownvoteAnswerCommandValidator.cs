using FluentValidation;

namespace Queans.Application.Answers.Commands.DownvoteAnswer
{
    public class DownvoteAnswerCommandValidator : AbstractValidator<DownvoteAnswerCommand>
    {
        public DownvoteAnswerCommandValidator()
        {
            RuleFor(command => command.UserId)
                .NotEmpty().WithMessage("Invalid user identifier");

            RuleFor(command => command.AnswerId)
                .NotEmpty().WithMessage("Invalid answer identifier");
        }
    }
}
