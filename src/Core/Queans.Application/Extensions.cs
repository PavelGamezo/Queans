using Microsoft.Extensions.DependencyInjection;
using MediatR;
using System.Reflection;
using Queans.Application.Common.Behaviors;
using FluentValidation;

namespace Queans.Application
{
    public static class Extensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped(
                typeof(IPipelineBehavior<,>),
                typeof(ValidationBehavior<,>));

            services.AddScoped(
                typeof(IPipelineBehavior<,>),
                typeof(LoggingBehavior<,>));

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
