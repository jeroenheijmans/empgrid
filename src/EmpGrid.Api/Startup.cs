using System;
using EmpGrid.Api.Auth;
using EmpGrid.Api.Models.Auth;
using EmpGrid.DataAccess.Core;
using EmpGrid.Domain;
using EmpGrid.Domain.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Swagger;

namespace EmpGrid.Api
{
    public class Startup
    {
        private const string ApiTitle = "EmpGrid API";

        public IConfigurationRoot Configuration { get; }
        public IHostingEnvironment Environment { get; }

        public Startup(IHostingEnvironment environment)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(environment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
            Environment = environment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureAuthentication(services);

            services.AddMvc();

            services.AddScoped<IBulkEntityRepository<Emp>, EmpRepository>();
            services.AddScoped<ISingularRepository<Medium>, MediumRepository>();

            services.ConfigureAutoMapper();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = ApiTitle, Version = "v1" });
            });

            services.AddSingleton<FirstTimeSetup>();
        }

        private void ConfigureAuthentication(IServiceCollection services)
        {
            services
                .AddIdentity<EmpGridUser, EmpGridRole>()
                .AddUserStore<EmpGridUserStore>()
                .AddRoleStore<EmpGridRoleStore>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 12;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
            });

            var identityServerBuilder = services
                .AddIdentityServer()
                .AddInMemoryClients(Clients.Get())
                .AddInMemoryApiResources(ApiResources.Get())
                .AddInMemoryIdentityResources(IdentityResources.Get())
                .AddAspNetIdentity<EmpGridUser>();

            if (Environment.IsDevelopment())
            {
                identityServerBuilder.AddDeveloperSigningCredential();
            }
            else
            {
                throw new NotImplementedException("Cannot configure services for prod environments yet: need to configure IdentityServer4 signing setup");
            }

            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = "Bearer";

                    // This is required to prevent redirection to /account/login when
                    // a token is not correct. Since we use the legacy Resource Owner
                    // Password flow, we don't use that. The result of this line is
                    // a 403 instead of a 404.
                    options.DefaultChallengeScheme = "Bearer";
                })
                .AddIdentityServerAuthentication(options =>
                {
                    // TODO: Grab from settings:
                    options.Authority = "http://localhost:65203";
                    options.ApiName = ApiResources.ApiResourceName;

                    // Only for development:
                    options.RequireHttpsMetadata = !Environment.IsDevelopment();
                });
        }

        public void Configure(
            IApplicationBuilder app, 
            IHostingEnvironment env, 
            ILoggerFactory loggerFactory,
            FirstTimeSetup firstTimeSetup)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var loggingSection = Configuration.GetSection("Logging");
            loggerFactory.AddConsole(loggingSection);
            loggerFactory.AddDebug(loggingSection.GetValue<LogLevel>("Default"));

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseSwagger(); // Json endpoint (required for UI)
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", ApiTitle); });

            app.UseIdentityServer();
            app.UseAuthentication();

            app.UseMvc();

            firstTimeSetup.Run().Wait();
        }
    }
}
