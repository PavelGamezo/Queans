using Queans.Domain.Common;

namespace Queans.Domain.Questions.Events
{
    public record AnswerAddedEvent(Guid AnswerId, Guid QuestionId) : IDomainEvent;
}
