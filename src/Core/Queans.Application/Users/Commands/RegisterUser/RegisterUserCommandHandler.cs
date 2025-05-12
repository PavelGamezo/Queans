using ErrorOr;
using Queans.Application.Common.CQRS.Commands;
using Queans.Application.Common.DTOs;
using Queans.Application.Common.Errors;
using Queans.Application.Common.Persistence;
using Queans.Domain.Users;

namespace Queans.Application.Users.Commands.RegisterUser
{
    public class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, ErrorOr<UserDto>>
    {
        private const int INITIAL_USER_RATING = 0;

        private readonly IUserRepository _userRepository;

        public RegisterUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ErrorOr<UserDto>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var (username, email, password) = request;

            if (await _userRepository.IsUserExistAsync(email, username, cancellationToken))
            {
                return ApplicationErrors.UserExistError;
            }

            // TODO: Implement password hashing 
            // var passwordHash = _passwordHashService.HashPassword(password);

            var userCreationResult = User.Create(
                username,
                email,
                password,
                INITIAL_USER_RATING);

            if (userCreationResult.IsError)
            {
                return userCreationResult.Errors;
            }

            var user = userCreationResult.Value;

            var role = await _userRepository.GetUserRoleAsync(cancellationToken);
            if (role is null)
            {
                return ApplicationErrors.RoleNotFoundError;
            }

            user.RegisterUserRole(role);

            await _userRepository.AddAsync(user, cancellationToken);

            return new UserDto(
                Id: user.Id,
                UserName: user.UserName,
                Email: user.UserEmail,
                Rating: user.Rating);
        }
    }
}
