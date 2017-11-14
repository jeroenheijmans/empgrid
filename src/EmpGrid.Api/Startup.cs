using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using EmpGrid.Domain;
using EmpGrid.Domain.Core;
using EmpGrid.DataAccess.Core;
using Swashbuckle.AspNetCore.Swagger;

namespace EmpGrid.Api
{
    public class Startup
    {
        private const string ApiTitle = "EmpGrid API";

        public Startup(IHostingEnvironment env)
        {
            EmpRepository.SeedFakeDatabase("ExampleDataSeed.json");

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services) {
            services.AddMvc();

            services.AddScoped<IBulkEntityRepository<Emp>, EmpRepository>();
            services.AddScoped<ISingularRepository<Medium>, MediumRepository>();
            services.ConfigureAutoMapper();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = ApiTitle, Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            var loggingSection = Configuration.GetSection("Logging");
            loggerFactory.AddConsole(loggingSection);
            loggerFactory.AddDebug(loggingSection.GetValue<LogLevel>("Default"));

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseSwagger(); // Json endpoint (required for UI)
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", ApiTitle); });

            app.UseMvc();
        }
    }
}
