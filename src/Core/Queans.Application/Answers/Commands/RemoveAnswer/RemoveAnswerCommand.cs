using ErrorOr;
using Queans.Application.Common.CQRS.Commands;

namespace Queans.Application.Answers.Commands.RemoveAnswer
{
    public record RemoveAnswerCommand(Guid AnswerId) : ICommand<ErrorOr<Success>>;
}
