using MediatR;
using Microsoft.Extensions.Logging;
using Queans.Domain.Users.Events;

namespace Queans.Application.Users.Events
{
    public class UserRegisteredNotificationHandler(ILogger<UserRegisteredNotificationHandler> logger)
        : INotificationHandler<UserRegisteredEvent>
    {
        public Task Handle(UserRegisteredEvent notification, CancellationToken cancellationToken)
        {
            logger.LogInformation(
                "User with userId {@UserId} was added to database with role",
                notification.user.Id);

            return Task.CompletedTask;
        }
    }
}
