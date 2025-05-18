using ErrorOr;
using Queans.Application.Common.Authentications;
using Queans.Application.Common.CQRS.Queries;
using Queans.Application.Common.DTOs;
using Queans.Application.Common.Errors;
using Queans.Application.Common.Persistence;
using Queans.Application.Common.Services;
using Queans.Domain.Users;

namespace Queans.Application.Users.Queries.LoginUser
{
    public class LoginUserQueryHandler : IQueryHandler<LoginUserQuery, ErrorOr<string>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IPasswordHasher _passwordHasher;

        public LoginUserQueryHandler(
            IUserRepository userRepository,
            IJwtTokenGenerator jwtTokenGenerator,
            IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
            _passwordHasher = passwordHasher;
        }

        public async Task<ErrorOr<string>> Handle(LoginUserQuery request, CancellationToken cancellationToken)
        {
            var (email, password) = request;

            var user = await _userRepository.GetUserByEmailAsync(email, cancellationToken);

            if (user is null)
            {
                return ApplicationErrors.NotFoundUser;
            }

            // Validate password with PasswordHashService
            if (!_passwordHasher.VerifyHashedPassword(password, user.PasswordHash))
            {
                return ApplicationErrors.NotFoundUser;
            }

            var token = _jwtTokenGenerator.GenerateToken(user);

            return token;
        }
    }
}
