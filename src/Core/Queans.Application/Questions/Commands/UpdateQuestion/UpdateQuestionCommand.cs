using ErrorOr;
using Queans.Application.Common.CQRS.Commands;
using Queans.Application.Common.DTOs;
using Queans.Domain.Questions;

namespace Queans.Application.Questions.Commands.UpdateQuestion
{
    public record UpdateQuestionCommand(QuestionDetailsDto request) : ICommand<ErrorOr<Success>>;
}
