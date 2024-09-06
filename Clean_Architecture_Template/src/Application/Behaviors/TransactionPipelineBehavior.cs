using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Behaviors
{
    internal sealed class TransactionPipelineBehavior<TRequest, TResponse> (
        IUnitOfWork unitofWork,
        ILogger<TransactionPipelineBehavior<TRequest, TResponse>> logger) : IPipelineBehavior<TRequest, TResponse> 
        where TRequest : class
    {
        public async Task<TResponse> Handle(TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            logger.LogInformation("Beginning transaction for {RequestName}", typeof(TRequest).Name);

            await using IDbContextTransaction transaction = await unitofWork.BeginTransactionAsync(cancellationToken);

            TResponse response = await next();

            await transaction.CommitAsync(cancellationToken);

            logger.LogInformation("Committed transaction for {RequestName}", typeof(TRequest).Name);

            return response;

        }
    }
}
