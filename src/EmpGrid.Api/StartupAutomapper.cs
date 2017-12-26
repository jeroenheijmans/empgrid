using AutoMapper;
using EmpGrid.Domain.Core;
using Microsoft.Extensions.DependencyInjection;

namespace EmpGrid.Api
{
    public static class StartupAutomapper 
    {
        public static void ConfigureAutoMapper(this IServiceCollection services)
        {
            InitializeMappers();

            services.AddSingleton(Mapper.Configuration);

            services.AddScoped<IMapper>(sp => new Mapper(
                sp.GetRequiredService<IConfigurationProvider>(),
                sp.GetService)
            );
        }

        public static void InitializeMappers()
        {
            // TODO: Organize in Profiles
            Mapper.Initialize(cfg => {
                cfg.CreateMap<Medium, Models.Core.MediumModel>();
                cfg.CreateMap<Emp, Models.Core.EmpModel>().ReverseMap();
                cfg.CreateMap<Presence, Models.Core.PresenceModel>().ReverseMap();
            });
        }
    }
}
