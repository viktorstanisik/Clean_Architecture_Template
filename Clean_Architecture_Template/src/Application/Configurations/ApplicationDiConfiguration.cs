namespace Application.Configurations;

public static class ApplicationDiConfiguration
{
    public static void RegisterApplicationServices(IServiceCollection services)
    {
        services.AddSingleton(typeof(IPasswordHasher), typeof(PasswordHasher));

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ApplicationDiConfiguration).Assembly));

        services.AddSingleton(TypeAdapterConfig.GlobalSettings);

    }
}