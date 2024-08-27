using Clean_Architecture_Template_Domain_Layer.Entities;

namespace Clean_Architecture_Template_Infrastructure_Layer;

public class CleanArchitectureTemplateDbContext(DbContextOptions<CleanArchitectureTemplateDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; init; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CleanArchitectureTemplateDbContext).Assembly);
    }
}