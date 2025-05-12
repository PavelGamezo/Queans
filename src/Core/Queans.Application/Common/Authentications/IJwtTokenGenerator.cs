using Queans.Domain.Users;

namespace Queans.Application.Common.Authentications
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user);
    }
}
