using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Authentification.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WebUtils.Middlewares;

namespace Authentification
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
            var identityConnectionString = Configuration.GetConnectionString("Identity");
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(identityConnectionString);
            });

            ConfigureIdentity(services, identityConnectionString);

            services.AddMvcCore()
                .AddDataAnnotations()
                .AddAuthorization()
                .AddVersionedApiExplorer(o => o.GroupNameFormat = "'v'V")
                .AddJsonFormatters()
                .AddCors();

            services.AddApiVersioning();

            services.Configure<ApplicationSettings>(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILogger<Startup> logger, IApiVersionDescriptionProvider provider, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            }

            //app.UseLogResponseMiddleware();
            app.UseJsonExceptionMiddleware(env);

            app.UseIdentityServer();

            app.Use((context, next) =>
            {
                logger.LogTrace($"Request to {context.Request.Path}");
                logger.LogTrace($"Response: {context.Response.StatusCode}");
                return next();
            });

            app.UseMvcWithDefaultRoute();
        }
        private void ConfigureIdentity(IServiceCollection services, string identityConnectionString)
        {
        }
    }
}
