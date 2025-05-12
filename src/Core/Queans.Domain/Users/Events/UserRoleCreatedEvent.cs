using Queans.Domain.Common;

namespace Queans.Domain.Users.Events
{
    public record UserRoleCreatedEvent(Guid UserId, int RoleId) : IDomainEvent
    {
    }
}
