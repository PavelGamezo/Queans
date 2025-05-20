using Queans.Domain.Common;

namespace Queans.Domain.Questions.Events
{
    public record AnswerCreatedEvent(Guid AnswerId) : IDomainEvent;
}
