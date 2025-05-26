using Queans.Application.Common.Persistence;
using Queans.Application.Common.Services;
using Queans.Domain.Users;
using Queans.Domain.Users.Entities;
using Queans.Infrastructure.Persistence.Contexts;

namespace Queans.Infrastructure.Persistence.SeedDatas
{
    public static class AdminSeeder
    {
        public static async Task SeedAsync(
            QueansDbContext dbContext,
            IPasswordHasher passwordHasher,
            IUserRepository userRepository)
        {
            if (!dbContext.Users.Any())
            {
                var adminEmail = "QueansAdmin@gmail.com";
                var passwordHash = passwordHasher.GenerateHashedPassword("administrator");
                var adminName = "Admin";
                var adminRating = 0;

                var userFactoryResult = User.Create(adminName, adminEmail, passwordHash, adminRating);
                if (userFactoryResult.IsError)
                {
                    throw new Exception("Can't create admin");
                }

                var adminUser = userFactoryResult.Value;
                var adminRole = await userRepository.GetAdminRoleAsync(CancellationToken.None);
                if (adminRole is null)
                {
                    throw new Exception("Can't create admin role");
                }

                adminUser.RegisterUserRole(adminRole);

                await dbContext.AddAsync(adminUser, CancellationToken.None);
                await dbContext.SaveChangesAsync(CancellationToken.None);
            }
        }
    }
}
