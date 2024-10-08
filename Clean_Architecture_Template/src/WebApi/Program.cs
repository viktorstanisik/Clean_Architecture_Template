using Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);
DiConfiguration.RegisterServices(builder.Services);

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

builder.Services.AddCarter();
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

//builder.Services.AddDbContext<CleanArchitectureTemplateDbContext>(options =>
//    options.UseInMemoryDatabase(databaseName: "CleanArchitectureTemplateDb"));

builder.Services.AddDbContext<CleanArchitectureTemplateDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseConnectionString")));


Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/log.txt",
        rollingInterval: RollingInterval.Day,
        restrictedToMinimumLevel: LogEventLevel.Information)
    .MinimumLevel.Information()
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Host.UseSerilog();  // This line ensures that all ILogger<T> logs are routed through Serilog

MapperConfiguration.ConfigureMappings();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler();

app.UseHttpsRedirection();

app.MapCarter();

app.Run();