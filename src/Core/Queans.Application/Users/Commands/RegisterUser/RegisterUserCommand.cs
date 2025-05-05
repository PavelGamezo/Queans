using MediatR;

namespace Queans.Application.Users.Commands.RegisterUser
{
    public record RegisterUserCommand(
        string UserName,
        string UserEmail,
        string Password) : IRequest;
}
