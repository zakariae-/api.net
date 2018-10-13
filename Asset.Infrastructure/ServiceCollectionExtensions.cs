using System;
using Asset.Core.Interfaces;
using Asset.Infrastructure.Data;
using Asset.Infrastructure.Repositories;
using Commun.Infrastructure.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Asset.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAsset(this IServiceCollection services, IConfiguration configuration, ServiceLifetime lifetime = ServiceLifetime.Transient)
        {
            AddDependencies(services, lifetime);
            AddConfiguration(services, configuration, lifetime);

            return services;
        }

        private static void AddDependencies(IServiceCollection services, ServiceLifetime lifeTime)
        {
            services.addService<IUserRepository, UserRepository>(lifeTime);
        }

        private static void AddConfiguration(this IServiceCollection services, IConfiguration configuration, ServiceLifetime lifetime)
        {
            services.AddDbConfiguration<AssetContext>(configuration, AssetSettings.SqlConnectionStringName, lifetime);
        }
    }
}
