namespace Application.Configurations;

public static class ApplicationDiConfiguration
{
    public static void RegisterApplicationServices(IServiceCollection services)
    {
        services.AddSingleton(typeof(IPasswordHasher), typeof(PasswordHasher));

        services.AddSingleton(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));

        services.AddSingleton(typeof(IPipelineBehavior<,>), typeof(LoggingPipelineBehavior<,>));

        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(ApplicationDiConfiguration).Assembly));

        services.AddSingleton(TypeAdapterConfig.GlobalSettings);
    }
}