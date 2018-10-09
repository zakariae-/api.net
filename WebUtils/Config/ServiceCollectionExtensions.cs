using System;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace WebUtils.Config
{
    public static class ServiceCollectionExtensions
    {
        public static void AddIdentityAuthentification(this IServiceCollection services, IHostingEnvironment environment, string audience, string authority)
        {
            services.AddAuthentication(o =>
            {
                o.DefaultScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
                o.DefaultAuthenticateScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
            })
                .AddIdentityServerAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme,
                    options =>
                    {
                        options.Audience = audience;
                        options.Authority = authority;
                        options.RequireHttpsMetadata = !environment.IsDevelopment();

                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = !environment.IsDevelopment(),
                            ValidateAudience = true,
                            ValidAudience = options.Audience,
                            ValidateLifetime = true,
                            ClockSkew = TimeSpan.Zero
                        };
                    }, null);
        }
    }
}
