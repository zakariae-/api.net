using System;
using Asset.Core.Interfaces;
using Asset.Infrastructure.Repositories;
using Commun.Infrastructure.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Asset.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAsset(this IServiceCollection services, ServiceLifetime lifetime = ServiceLifetime.Transient)
        {
            AddDependencies(services, lifetime);

            return services;
        }

        private static void AddDependencies(IServiceCollection services, ServiceLifetime lifeTime)
        {
            services.addService<IUserRepository, UserRepository>(lifeTime);
        }
    }
}
