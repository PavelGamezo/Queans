using Queans.Domain.Common;
using Queans.Domain.Questions.Entities;

namespace Queans.Domain.Questions.Events
{
    public record class QuestionTagAddedEvent(Tag tag) : IDomainEvent;
}
