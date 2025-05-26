using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Queans.Application.Common.Persistence;
using Queans.Application.Common.Services;
using Queans.Infrastructure.Common.Options;
using Queans.Infrastructure.Persistence.Contexts;
using Queans.Infrastructure.Persistence.Interceptors;
using Queans.Infrastructure.Persistence.Repositories;
using Queans.Infrastructure.Persistence.SeedDatas;

namespace Queans.Infrastructure.Persistence
{
    public static class Extensions
    {
        public static IServiceCollection AddPersistence(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString = new ConnectionString();
            configuration.Bind(ConnectionString.SectionName, connectionString);

            services.AddSingleton(Options.Create(connectionString));

            // Added DbContext
            services.AddDbContext<QueansDbContext>(options =>
            {
                options.UseNpgsql(connectionString.Value);
            });

            services.AddScoped<PublishDomainEventsInterseptor>();

            // Added repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IQuestionRepository, QuestionRepository>();
            services.AddScoped<IAnswerRepository, AnswerRepository>();
            services.AddScoped<ITagRepository, TagRepository>();

            return services;
        }

        public static async Task<WebApplication> UseDatabaseSeeding(this WebApplication app)
        {
            using (var scope = app.Services.CreateAsyncScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<QueansDbContext>();
                var passwordHasher = scope.ServiceProvider.GetRequiredService<IPasswordHasher>();
                var userRepository = scope.ServiceProvider.GetRequiredService<IUserRepository>();

                dbContext.Database.Migrate();
                await AdminSeeder.SeedAsync(dbContext, passwordHasher, userRepository);
            }

            return app;
        }
    }
}
