using Queans.Domain.Users;
using Queans.Domain.Users.Entities;

namespace Queans.Application.Common.Persistence
{
    public interface IUserRepository
    {
        Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken);

        Task<Role?> GetUserRoleAsync(CancellationToken cancellationToken);

        Task<bool> IsUserExistAsync(string email, string username, CancellationToken cancellationToken);

        Task<User?> GetUserByIdAsync(Guid id, CancellationToken cancellationToken);

        Task<User?> GetUserByUserNameAsync(string userName, CancellationToken cancellationToken);

        Task AddAsync(User user, CancellationToken cancellationToken);

        Task RemoveAsync(User user, CancellationToken cancellationToken);

        Task UpdateAsync(User user, CancellationToken cancellationToken);

        Task SaveAsync(CancellationToken cancellationToken);
    }
}
