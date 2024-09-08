namespace Application.Behaviors
{
    internal sealed class TransactionPipelineBehavior<TRequest, TResponse>(
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

            try
            {
                var response = await next();

                if (IsResponseOfTypeResult())
                {
                    return await ProcessResult(response, transaction, cancellationToken);
                }

                await CommitTransaction(transaction, cancellationToken);

                return response;
            }
            catch (Exception e)
            {
                await transaction.RollbackAsync(cancellationToken);

                throw;
            }
        }


        private static bool IsResponseOfTypeResult()
        {
            return typeof(TResponse).IsGenericType && typeof(TResponse).GetGenericTypeDefinition() == typeof(Result<>);
        }

        private async Task<TResponse> ProcessResult(TResponse response, IDbContextTransaction transaction, CancellationToken cancellationToken)
        {
            var resultInterface = ConvertTResponseToResult();

            var successProperty = resultInterface.GetProperty(nameof(Result<TResponse>.Success));
            var errorMessage = resultInterface.GetProperty(nameof(Result<TResponse>.ErrorMessage));

            if (ResultIsFailure(successProperty, response))
            {
                logger.LogWarning("Transaction {transaction.TransactionId} rolled back for {RequestName} due to failure: {ErrorMessage}",
                                typeof(TRequest).Name, errorMessage);

                await transaction.RollbackAsync(cancellationToken);

                return response;

            }

            await CommitTransaction(transaction, cancellationToken);

            return response;

        }

        private Type ConvertTResponseToResult()
        {
            var resultType = typeof(TResponse).GetGenericArguments()[0]; // Extract the T in Result<T>

            var resultInterface = typeof(Result<>).MakeGenericType(resultType);
            return resultInterface;
        }

        private static bool ResultIsFailure(PropertyInfo? successProperty, TResponse response)
        {
            return successProperty != null && !(bool)successProperty.GetValue(response);
        }


        private async Task CommitTransaction(IDbContextTransaction transaction, CancellationToken cancellationToken)
        {
            await transaction.CommitAsync(cancellationToken);

            logger.LogInformation("Committed transaction {transaction.TransactionId} for {RequestName}", typeof(TRequest).Name);
        }
    }
}
