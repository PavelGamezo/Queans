using ErrorOr;
using Queans.Application.Common.CQRS.Commands;
using System.Windows.Input;

namespace Queans.Application.Answers.Commands.DownvoteAnswer
{
    public record DownvoteAnswerCommand(
        Guid AnswerId,
        Guid UserId) : ICommand<ErrorOr<Success>>;
}
