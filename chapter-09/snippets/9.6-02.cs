// Modern .NET, Chapter 9, Section 9.6
public sealed class NotFoundException(string message) : Exception(message);
public sealed class ConflictException(string message) : Exception(message);

public sealed class ApiExceptionHandler(ILogger<ApiExceptionHandler> logger, IProblemDetailsService problems) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken ct)
    {
        var (status, title) = exception switch
        {
            NotFoundException   => (StatusCodes.Status404NotFound, "Resource not found"),
            ConflictException   => (StatusCodes.Status409Conflict, "Conflict"),
            OperationCanceledException => (499, "Client closed request"),
            _                   => (StatusCodes.Status500InternalServerError, "An unexpected error occurred")
        };

        if (status == 500) logger.LogError(exception, "Unhandled exception");

        context.Response.StatusCode = status;
        return await problems.TryWriteAsync(new ProblemDetailsContext
        {
            HttpContext = context,
            Exception = exception,
            ProblemDetails = new ProblemDetails
            {
                Status = status,
                Title = title,
                Detail = status == 500 ? null : exception.Message   // never leak internals on 500
            }
        });
    }
}
