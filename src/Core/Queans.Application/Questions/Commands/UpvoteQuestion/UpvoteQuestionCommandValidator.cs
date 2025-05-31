using FluentValidation;

namespace Queans.Application.Questions.Commands.UpvoteQuestion
{
    public class UpvoteQuestionCommandValidator : AbstractValidator<UpvoteQuestionCommand>
    {
        public UpvoteQuestionCommandValidator()
        {
            RuleFor(command => command.UserId)
                .NotEmpty().WithMessage("Invalid user identifier");

            RuleFor(command => command.QuestionId)
                .NotEmpty().WithMessage("Invalid question identifier");
        }
    }
}
