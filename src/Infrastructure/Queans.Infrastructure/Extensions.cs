using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Queans.Application.Common.Authentications;
using Queans.Application.Common.Persistence;
using Queans.Application.Common.Services;
using Queans.Infrastructure.Authentication;
using Queans.Infrastructure.Common.Options;
using Queans.Infrastructure.Persistence.Contexts;
using Queans.Infrastructure.Persistence.Interceptors;
using Queans.Infrastructure.Persistence.Repositories;
using Queans.Infrastructure.Services;
using System.Text;

namespace Queans.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddScoped<IPasswordHasher, PasswordHasher>();

            services.AddPersistence(configuration);

            services.AddJwtAuthentication(configuration);

            return services;
        }

        public static IServiceCollection AddJwtAuthentication(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var jwtOptions = new JwtSettings();
            configuration.Bind(JwtSettings.SectionName, jwtOptions);

            services.AddSingleton(Options.Create(jwtOptions));

            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = false,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtOptions.Issuer,
                        ValidAudience = jwtOptions.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(jwtOptions.Secret))
                    };

                    //options.Events = new JwtBearerEvents()
                    //{
                    //    OnMessageReceived = context =>
                    //    {
                    //        context.Token = context.Request.Cookies["queans-cookies"];

                    //        return Task.CompletedTask;
                    //    }
                    //};
                });

            return services;
        }

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

            return services;
        }
    }
}
