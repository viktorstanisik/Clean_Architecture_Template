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
