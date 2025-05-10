using Queans.Api.Common.GlobalExceptions;

namespace Queans.Api
{
    public static class Extensions
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddTransient<GlobalExceptionHandler>();

            services.AddControllers();

            services.AddOpenApi();

            return services;
        }
    }
}
