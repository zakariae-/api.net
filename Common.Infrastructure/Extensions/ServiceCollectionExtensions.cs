using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Commun.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection addService<TService, TImplementation>(this IServiceCollection services, ServiceLifetime lifetime)
            where TService : class
            where TImplementation : class, TService
        {
            switch (lifetime)
            {
                case ServiceLifetime.Scoped:
                    services.AddScoped<TService, TImplementation>();
                    break;
                case ServiceLifetime.Singleton:
                    services.AddSingleton<TService, TImplementation>();
                    break;
                case ServiceLifetime.Transient:
                    services.AddTransient<TService, TImplementation>();
                    break;
                default:
                    NotImplementedException notImplementedException = new NotImplementedException($"Unknown lifetime {lifetime}");
                    throw notImplementedException;
            }
            return services;
        }

        public static void AddDbConfiguration<TDbContext>(this IServiceCollection services,
            IConfiguration configuration,
            string connectionStringName,
            ServiceLifetime lifeTime) where TDbContext : DbContext
        {
            var connectionString = configuration.GetConnectionString(connectionStringName);
            if (connectionString == null)
            {
                throw new Exception($"Missing configuration : ConnectionStrings.{connectionStringName}");
            }

            services.AddEntityFrameworkSqlServer()
                .AddDbContext<TDbContext>(options =>
                {
                    options.UseSqlServer(connectionString);
                }, lifeTime);
        }
    }
}