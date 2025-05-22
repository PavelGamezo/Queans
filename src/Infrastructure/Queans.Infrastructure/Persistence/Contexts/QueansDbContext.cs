using Microsoft.EntityFrameworkCore;
using Queans.Domain.Common;
using Queans.Domain.Questions;
using Queans.Domain.Questions.Entities;
using Queans.Domain.Users;
using Queans.Domain.Users.Entities;
using Queans.Infrastructure.Persistence.Configurations;
using Queans.Infrastructure.Persistence.Interceptors;

namespace Queans.Infrastructure.Persistence.Contexts
{
    public class QueansDbContext : DbContext
    {
        private readonly PublishDomainEventsInterseptor _publishDomainEventsInterseptor;

        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<Question> Questions { get; set; }

        public DbSet<Answer> Answers { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public QueansDbContext(DbContextOptions options,
            PublishDomainEventsInterseptor publishDomainEventsInterseptor) : base(options)
        {
            _publishDomainEventsInterseptor = publishDomainEventsInterseptor;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var userConfiguration = new UserConfiguration();
            var roleConfiguration = new RoleConfiguration();
            var questionConfiguration = new QuestionConfiguration();
            var answerConfiguration = new AnswerConfiguration();

            modelBuilder
                .Ignore<List<IDomainEvent>>()
                .ApplyConfiguration(userConfiguration)
                .ApplyConfiguration(roleConfiguration)
                .ApplyConfiguration(questionConfiguration)
                .ApplyConfiguration(answerConfiguration);

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(_publishDomainEventsInterseptor);
            base.OnConfiguring(optionsBuilder);
        }
    }
}
