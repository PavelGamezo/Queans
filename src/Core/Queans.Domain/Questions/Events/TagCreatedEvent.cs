using Queans.Domain.Common;

namespace Queans.Domain.Questions.Events
{
    public record TagCreatedEvent(Guid TagId) : IDomainEvent;
}
