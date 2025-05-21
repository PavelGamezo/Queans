using ErrorOr;
using Queans.Application.Common.CQRS.Commands;

namespace Queans.Application.Answers.Commands.CreateAnswer
{
    public record CreateAnswerCommand(
        string Text,
        Guid QuestionId,
        Guid AuthorId) : ICommand<ErrorOr<Success>>;
}
