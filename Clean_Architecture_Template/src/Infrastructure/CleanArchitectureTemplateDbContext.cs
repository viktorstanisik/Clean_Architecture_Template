namespace Infrastructure;

public class CleanArchitectureTemplateDbContext(DbContextOptions<CleanArchitectureTemplateDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; init; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CleanArchitectureTemplateDbContext).Assembly);
    }
}