using System;
using Microsoft.Extensions.DependencyInjection;
using Asset.Services.Interface;
using Asset.Services.Services;

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
