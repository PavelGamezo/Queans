using MediatR;

namespace Queans.Application.Users.Commands.RegisterUser
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand>
    {
        public Task Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var (userName, userEmail, password) = request;

            return Task.CompletedTask;
        }
    }
}
