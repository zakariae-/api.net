using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Asset.Infrastructure;
using Asset.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WebUtils.Middlewares;

namespace WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }

        public IHostingEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // ASP.NET Core
            services.AddMvc(options =>
            {
                options.CacheProfiles.Add("default",
                new CacheProfile
                {
                    Location = ResponseCacheLocation.Client,
                    NoStore = true
                });
            });

            services.AddMvcCore()
                .AddVersionedApiExplorer(o =>
                {
                    o.GroupNameFormat = "'v'V";
                    o.SubstituteApiVersionInUrl = true;
                })
                .AddJsonFormatters();

            services.AddApiVersioning();

            services.AddCors();
            services.AddOptions();

            services.Configure<ApplicationSettings>(Configuration);

            services.AddAsset(Configuration, ServiceLifetime.Scoped);
            services.AddAssetServices();

            ConfigureAuth(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILogger<Startup> logger, IApiVersionDescriptionProvider provider, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddApplicationInsights(app.ApplicationServices, LogLevel.Information);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }

            /* ORDER MATTER! Cors must be before mvc */
            app.UseCors(builder =>
                builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
            );

            app.UseLogResponseMiddleware();
            app.UseJsonExceptionMiddleware(env);

            app.UseAuthentication();

            app.UseStaticFiles();

            app.UseMvcWithDefaultRoute();
        }

        private void ConfigureAuth(IServiceCollection services)
        {

        }
    }
}
