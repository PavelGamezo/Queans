using FluentValidation;

namespace Queans.Application.Questions.Commands.DownvoteQuestion
{
    public class DownvoteQuestionCommandValidator : AbstractValidator<DownvoteQuestionCommand>
    {
        public DownvoteQuestionCommandValidator()
        {
            RuleFor(command => command.QuestionId)
                .NotEmpty().WithMessage("Can't get question identifier");

            RuleFor(command => command.UserId)
                .NotEmpty().WithMessage("Can't get user identifier");
        }
    }
}
