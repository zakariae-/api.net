using System;
using Microsoft.Extensions.DependencyInjection;

namespace Commun.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection addService<TService, TImplementation>(this IServiceCollection services, ServiceLifetime lifetime)
            where TService : class
            where TImplementation: class, TService
        {
            switch(lifetime)
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
    }
}
