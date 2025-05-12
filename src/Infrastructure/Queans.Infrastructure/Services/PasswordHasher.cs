using Queans.Application.Common.Services;

namespace Queans.Infrastructure.Services
{
    public class PasswordHasher : IPasswordHasher
    {
        public string GenerateHashedPassword(string password)
            => BCrypt.Net.BCrypt.EnhancedHashPassword(password);

        public bool VerifyHashedPassword(string password, string hashedPassword)
            => BCrypt.Net.BCrypt.EnhancedVerify(password, hashedPassword);
    }
}
