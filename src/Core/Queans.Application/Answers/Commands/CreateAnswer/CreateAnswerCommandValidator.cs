using FluentValidation;

namespace Queans.Application.Answers.Commands.CreateAnswer
{
    public class CreateAnswerCommandValidator : AbstractValidator<CreateAnswerCommand>
    {
        public CreateAnswerCommandValidator() 
        {
            RuleFor(command => command.Text)
                .NotEmpty().WithMessage("Text can't be empty")
                .MinimumLength(1).WithMessage("Text must have more than one character");

            RuleFor(command => command.AuthorId)
                .NotEmpty().WithMessage("Invalid author identifier");

            RuleFor(command => command.QuestionId)
                .NotEmpty().WithMessage("Invalid question identifier");
        }
    }
}
