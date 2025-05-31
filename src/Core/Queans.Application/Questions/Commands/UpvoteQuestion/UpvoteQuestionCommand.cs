using ErrorOr;
using Queans.Application.Common.CQRS.Commands;

namespace Queans.Application.Questions.Commands.UpvoteQuestion
{
    public record UpvoteQuestionCommand(
        Guid QuestionId,
        Guid UserId) : ICommand<ErrorOr<Success>>;
}
