using Clean_Architecture_Template_Application_Layer.Behaviors;
using Clean_Architecture_Template_Infrastructure_Layer.Configurations.DiConfigurations;
using Mapster;
using MediatR;

namespace Clean_Architecture_Template_WebApi.Configuration
{
    public static class DiConfiguration
    {
        public static void RegisterServices(IServiceCollection services)
        {
            ApplicationDiConfiguration.RegisterApplicationServices(services);

            InfrastructureDiConfiguration.RegisterInfrastructureServices(services);


        }
    }
}
