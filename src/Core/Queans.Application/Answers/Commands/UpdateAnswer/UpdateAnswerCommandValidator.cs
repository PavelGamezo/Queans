using FluentValidation;

namespace Queans.Application.Answers.Commands.UpdateAnswer
{
    public class UpdateAnswerCommandValidator : AbstractValidator<UpdateAnswerCommand>
    {
        public UpdateAnswerCommandValidator()
        {
            RuleFor(command => command.Id)
                .NotEmpty().WithMessage("Invalid answer identifier");

            RuleFor(command => command.Text)
                .NotEmpty().WithMessage("Text can't be empty")
                .MinimumLength(5).WithMessage("Text length must be more than one character");
        }
    }
}
