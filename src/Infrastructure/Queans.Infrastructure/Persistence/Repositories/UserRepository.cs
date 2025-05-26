using Microsoft.EntityFrameworkCore;
using Queans.Application.Common.Persistence;
using Queans.Domain.Users;
using Queans.Domain.Users.Entities;
using Queans.Domain.Users.Enums;
using Queans.Infrastructure.Persistence.Contexts;

namespace Queans.Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly QueansDbContext _dbContext;

        public UserRepository(QueansDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(User user, CancellationToken cancellationToken)
        {
            await _dbContext.Users.AddAsync(user, cancellationToken);
            await SaveAsync(cancellationToken);
        }

        public async Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken)
        {
            return await _dbContext.Users
                .Include(user => user.Roles)
                .SingleOrDefaultAsync(
                    user => user.UserEmail == email, cancellationToken);
        }

        public async Task<User?> GetUserByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _dbContext.Users
                .Include(user => user.Roles)
                .Include(user => user.Answers)
                .Include(user => user.Questions)
                .SingleOrDefaultAsync(
                    user => user.Id == id, cancellationToken);
        }

        public async Task<User?> GetUserByUserNameAsync(string userName, CancellationToken cancellationToken)
        {
            return await _dbContext.Users
                .Include (user => user.Roles)
                .SingleOrDefaultAsync(
                    user => user.UserName == userName,
                    cancellationToken);
        }

        public async Task<bool> IsUserExistAsync(string email, string username, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users
                .SingleOrDefaultAsync(
                    user => user.UserEmail == email,
                    cancellationToken);

            if (user is null || user.UserName != username)
            {
                return false;
            }

            return true;
        }

        public async Task RemoveAsync(User user, CancellationToken cancellationToken)
        {

            _dbContext.Users.Remove(user);
            await SaveAsync(cancellationToken);
        }

        public async Task SaveAsync(CancellationToken cancellationToken)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(User user, CancellationToken cancellationToken)
        {
            _dbContext.Update(user);
            await SaveAsync(cancellationToken);
        }

        public async Task<Role?> GetUserRoleAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.Roles
                .SingleOrDefaultAsync(
                    role => role.Name == RoleEnum.User.ToString(),
                    cancellationToken);
        }

        public async Task<Role?> GetAdminRoleAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.Roles
                .SingleOrDefaultAsync(
                    role => role.Name == RoleEnum.Admin.ToString(),
                    cancellationToken);
        }
    }
}
