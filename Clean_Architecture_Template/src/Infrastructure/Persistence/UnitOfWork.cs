namespace Infrastructure.Persistence;

public class UnitOfWork(CleanArchitectureTemplateDbContext dbContext) : IUnitOfWork
{
    private readonly CleanArchitectureTemplateDbContext _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

    public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.Database.BeginTransactionAsync(cancellationToken);

    }
    public async Task CommitTransactionAsync(CancellationToken cancellationToken)
    {
        if (_dbContext.Database.CurrentTransaction is null)
            throw new InvalidOperationException("No active transaction to commit.");

        await _dbContext.SaveChangesAsync(cancellationToken);
        await _dbContext.Database.CurrentTransaction.CommitAsync(cancellationToken);
    }

    // Rollback the current transaction
    public async Task RollbackTransactionAsync(CancellationToken cancellationToken)
    {
        if (_dbContext.Database.CurrentTransaction is null)
            throw new InvalidOperationException("No active transaction to rollback.");

        await _dbContext.Database.CurrentTransaction.RollbackAsync(cancellationToken);
    }

    // Save the changes in DbContext
    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    // Dispose of the DbContext
    public void Dispose()
    {
        _dbContext.Dispose();
    }
}