namespace Application.Configurations;

public static class ApplicationDiConfiguration
{
    public static void RegisterApplicationServices(IServiceCollection services)
    {
        services.AddSingleton(typeof(IPasswordHasher), typeof(PasswordHasher));

        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingPipelineBehavior<,>));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(TransactionPipelineBehavior<,>));

        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(ApplicationDiConfiguration).Assembly));

        services.AddSingleton(TypeAdapterConfig.GlobalSettings);
    }
}