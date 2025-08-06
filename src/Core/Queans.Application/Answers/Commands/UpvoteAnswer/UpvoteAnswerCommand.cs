using ErrorOr;
using Queans.Application.Common.CQRS.Commands;

namespace Queans.Application.Answers.Commands.UpvoteAnswer
{
    public record UpvoteAnswerCommand(
        Guid AnswerId,
        Guid UserId) : ICommand<ErrorOr<Success>>;
}
