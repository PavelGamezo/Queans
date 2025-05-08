using MediatR;
using Queans.Domain.Users.Events;

namespace Queans.Application.Users.Events
{
    public class UserRegisteredNotificationHandler : INotificationHandler<UserRegisteredEvent>
    {
        public Task Handle(UserRegisteredEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
