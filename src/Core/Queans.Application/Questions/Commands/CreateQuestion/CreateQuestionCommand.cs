using Queans.Application.Common.CQRS.Commands;

namespace Queans.Application.Questions.Commands.CreateQuestion
{
    public record CreateQuestionCommand(
        string Title,
        string Description, 
        Guid AuthorId) : ICommand<Guid>;
}
