using Microsoft.EntityFrameworkCore;
using Queans.Domain.Users;
using Queans.Infrastructure.Persistence.Configurations;

namespace Queans.Infrastructure.Persistence.Contexts
{
    public class QueansDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public QueansDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var userConfiguration = new UserConfiguration();

            modelBuilder.ApplyConfiguration(userConfiguration);

            base.OnModelCreating(modelBuilder);
        }
    }
}
