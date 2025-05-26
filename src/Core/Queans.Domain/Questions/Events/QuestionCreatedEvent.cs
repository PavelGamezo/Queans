using Queans.Domain.Common;

namespace Queans.Domain.Questions.Events
{
    public record QuestionCreatedEvent(Question Question) : IDomainEvent;
}
