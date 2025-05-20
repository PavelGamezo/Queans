using Queans.Domain.Common;

namespace Queans.Domain.Questions.Events
{
    public record class TagAddedEvent(Guid TagId, Guid QuestionId) : IDomainEvent;
}
