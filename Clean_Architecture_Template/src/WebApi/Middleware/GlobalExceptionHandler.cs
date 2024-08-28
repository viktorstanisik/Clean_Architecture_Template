namespace WebApi.Middleware;

public class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is ValidationException validationException)
        {
            // Log the validation exception
            Log.Error("Validation Error: {Message}", validationException.Message);

            var problemDetails = new ValidationProblemDetails(validationException.Errors.ToDictionary(
                e => e.PropertyName,
                e => new[] { e.ErrorMessage }
            ))
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Validation Error"
            };

            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }

        // Handle other exceptions as internal server errors
        Log.Error(exception, "An unexpected error occurred");

        var serverErrorDetails = new ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Title = "An unexpected error occurred",
            Detail = exception.Message
        };

        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

        await httpContext.Response.WriteAsJsonAsync(serverErrorDetails, cancellationToken);

        return true;
    }
}