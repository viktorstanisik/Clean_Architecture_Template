namespace Infrastructure.Persistence;

public class UnitOfWork(CleanArchitectureTemplateDbContext dbContext) : IUnitOfWork
{
    private readonly CleanArchitectureTemplateDbContext _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

    public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.Database.BeginTransactionAsync(cancellationToken);

    }
    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
    public void Dispose()
    {
        _dbContext.Dispose();
    }
}