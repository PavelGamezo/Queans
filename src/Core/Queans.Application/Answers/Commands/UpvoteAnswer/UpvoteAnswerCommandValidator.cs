using FluentValidation;

namespace Queans.Application.Answers.Commands.UpvoteAnswer
{
    public class UpvoteAnswerCommandValidator : AbstractValidator<UpvoteAnswerCommand>
    {
        public UpvoteAnswerCommandValidator()
        {
            RuleFor(command => command.UserId)
                .NotEmpty().WithMessage("Invalid user identifier");

            RuleFor(command => command.AnswerId)
                .NotEmpty().WithMessage("Invalid answer identifier");
        }
    }
}
