using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Queans.Infrastructure.Common.Options;
using Queans.Infrastructure.Persistence.Contexts;

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

            services.AddDbContext<QueansDbContext>(options =>
            {
                options.UseNpgsql(connectionString.Value);
            });

            return services;
        }
    }
}
