namespace Application.Behaviors;

public class ValidationPipelineBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, Result<TResponse>>
    where TRequest : IRequest<Result<TResponse>>
{
    public async Task<Result<TResponse>> Handle(TRequest request, RequestHandlerDelegate<Result<TResponse>> next,
        CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request);

        // Run the validators
        var failures = validators
            .Select(v => v.Validate(context))
            .SelectMany(result => result.Errors)
            .Where(f => f != null)
            .ToList();

        // If validation fails, return a failure result with concatenated error messages
        if (failures.Any())
        {
            var failureMessages = string.Join("; ", failures.Select(f => f.ErrorMessage));
            return Result<TResponse>.CreateFailure(failureMessages);
        }

        // Proceed to the next handler if validation passes
        return await next();
    }
}