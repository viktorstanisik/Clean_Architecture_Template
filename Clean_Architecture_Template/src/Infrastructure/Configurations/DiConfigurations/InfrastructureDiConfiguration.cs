namespace Infrastructure.Configurations.DiConfigurations;

public static class InfrastructureDiConfiguration
{
    public static void RegisterInfrastructureServices(IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        // Register other infrastructure services here
    }
}