using ErrorOr;
using Queans.Application.Common.CQRS.Commands;
using Queans.Application.Common.DTOs;

namespace Queans.Application.Answers.Commands.UpdateAnswer
{
    public record UpdateAnswerCommand(Guid Id, string Text) : ICommand<ErrorOr<Success>>;
}
