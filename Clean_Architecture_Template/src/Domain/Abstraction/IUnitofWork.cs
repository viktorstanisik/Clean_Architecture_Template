

using Microsoft.EntityFrameworkCore.Storage;

namespace Domain.Abstraction;

public interface IUnitOfWork : IDisposable
{
    Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken);
    Task CommitTransactionAsync(CancellationToken cancellationToken);
    Task RollbackTransactionAsync(CancellationToken cancellationToken);
    Task SaveChangesAsync(CancellationToken cancellationToken);

}