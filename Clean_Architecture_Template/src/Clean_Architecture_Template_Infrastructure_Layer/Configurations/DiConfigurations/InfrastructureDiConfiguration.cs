using Clean_Architecture_Template_Infrastructure_Layer.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Clean_Architecture_Template_Infrastructure_Layer.Configurations.DiConfigurations;

public static class InfrastructureDiConfiguration
{
    public static void RegisterInfrastructureServices(IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        // Register other infrastructure services here
    }
}