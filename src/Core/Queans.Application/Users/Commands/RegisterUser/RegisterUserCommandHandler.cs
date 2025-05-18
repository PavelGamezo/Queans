using ErrorOr;
using Queans.Application.Common.CQRS.Commands;
using Queans.Application.Common.DTOs;
using Queans.Application.Common.Errors;
using Queans.Application.Common.Persistence;
using Queans.Application.Common.Services;
using Queans.Domain.Users;

namespace Queans.Application.Users.Commands.RegisterUser
{
    public class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, ErrorOr<UserDto>>
    {
        private const int INITIAL_USER_RATING = 0;

        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;

        public RegisterUserCommandHandler(
            IUserRepository userRepository,
            IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<ErrorOr<UserDto>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var (username, email, password) = request;

            if (await _userRepository.IsUserExistAsync(email, username, cancellationToken))
            {
                return ApplicationErrors.UserExistError;
            }

            var passwordHash = _passwordHasher.GenerateHashedPassword(password);

            var userCreationResult = User.Create(
                username,
                email,
                passwordHash,
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
