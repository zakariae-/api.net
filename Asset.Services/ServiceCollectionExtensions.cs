using System;
using Asset.Services.Interfaces;
using Asset.Services.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Asset.Services
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAssetServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}
