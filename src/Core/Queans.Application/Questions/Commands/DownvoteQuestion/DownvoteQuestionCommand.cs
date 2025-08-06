using ErrorOr;
using Queans.Application.Common.CQRS.Commands;

namespace Queans.Application.Questions.Commands.DownvoteQuestion
{
    public record DownvoteQuestionCommand(Guid QuestionId, Guid UserId) : ICommand<ErrorOr<Success>>;
}
