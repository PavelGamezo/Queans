using Queans.Domain.Common;

namespace Queans.Domain.Users.Events
{
    public record UserRegisteredEvent(User user) : IDomainEvent;
}
