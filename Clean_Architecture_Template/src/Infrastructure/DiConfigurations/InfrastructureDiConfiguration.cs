

namespace Infrastructure.DiConfigurations;

public static class InfrastructureDiConfiguration
{
    public static void RegisterInfrastructureServices(IServiceCollection services)
    {
        // Register other infrastructure services here

        services.AddScoped(typeof(IUserRepository), typeof(UserRepository));

        services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
    }
}