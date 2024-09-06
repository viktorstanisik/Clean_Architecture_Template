using Infrastructure.DiConfigurations;

namespace WebApi.Configuration
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
