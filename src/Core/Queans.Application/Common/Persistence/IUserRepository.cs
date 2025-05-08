using Queans.Domain.Users;

namespace Queans.Application.Common.Persistence
{
    public interface IUserRepository
    {
        Task<User> GetUserByEmailAsync(string email, CancellationToken cancellationToken);

        Task<User> GetUserByUserNameAsync(string userName, CancellationToken cancellationToken);

        Task AddAsync(User user, CancellationToken cancellationToken);

        Task RemoveAsync(User user, CancellationToken cancellationToken);

        Task UpdateAsync(User user, CancellationToken cancellationToken);

        Task SaveAsync(CancellationToken cancellationToken);
    }
}
