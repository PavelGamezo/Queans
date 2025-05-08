using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Queans.Application.Common.Persistence;
using Queans.Infrastructure.Common.Options;
using Queans.Infrastructure.Persistence.Contexts;
using Queans.Infrastructure.Persistence.Interceptors;
using Queans.Infrastructure.Persistence.Repositories;

namespace Queans.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            // create options object
            var connectionString = new ConnectionString();
            configuration.Bind(ConnectionString.SectionName, connectionString);

            // Add options
            services.AddSingleton(Options.Create(connectionString));

            services.AddPersistence(connectionString.Value);

            return services;
        }

        public static IServiceCollection AddPersistence(
            this IServiceCollection services,
            string connectionString)
        {
            // Added DbContext
            services.AddDbContext<QueansDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });

            // Added services
            services.AddScoped<PublishDomainEventsInterseptor>();

            // Added repositories
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
