using MediatR;
using Microsoft.Extensions.Logging;
using Queans.Domain.Users.Events;

namespace Queans.Application.Users.Events
{
    public class UserRoleCreatedNotificationHandler(ILogger<UserRoleCreatedNotificationHandler> logger)
        : INotificationHandler<UserRoleCreatedEvent>
    {
        public Task Handle(UserRoleCreatedEvent notification, CancellationToken cancellationToken)
        {
            logger.LogInformation("User {UserId} was added with role {RoleId}", 
                notification.UserId,
                notification.RoleId);

            return Task.CompletedTask;
        }
    }
}
