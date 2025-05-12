namespace Queans.Application.Common.Services
{
    public interface IPasswordHasher
    {
        string GenerateHashedPassword(string password);

        bool VerifyHashedPassword(string password, string hashedPassword);
    }
}
