using ErrorOr;
using Queans.Application.Common.CQRS.Commands;
using Queans.Application.Common.DTOs;

namespace Queans.Application.Users.Commands.RegisterUser
{
    public record RegisterUserCommand(
        string UserName,
        string UserEmail,
        string Password) : ICommand<ErrorOr<UserDto>>;
}
