using Queans.Domain.Common;

namespace Queans.Domain.Users.Entities
{
    public class Role : Entity<int>
    {
        private readonly List<User> _users = [];

        public IReadOnlyCollection<User> Users => _users;

        public string Name { get; init; } = null!;


        public Role(int Id) : base(Id)
        {
            
        }
    }
}
