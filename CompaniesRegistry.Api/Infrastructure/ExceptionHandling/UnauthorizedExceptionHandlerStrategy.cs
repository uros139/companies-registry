using Microsoft.AspNetCore.Mvc;

namespace CompaniesRegistry.Api.Infrastructure.ExceptionHandling;

public class UnauthorizedExceptionHandlerStrategy(ILogger<UnauthorizedExceptionHandlerStrategy> logger) : IExceptionHandlerStrategy
{
    public bool CanHandle(Exception exception) => exception is UnauthorizedAccessException;

    public async ValueTask HandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
    {
        logger.LogWarning(exception, "Unauthorized access");

        var problemDetails = new ProblemDetails
        {
            Status = StatusCodes.Status401Unauthorized,
            Type = "https://datatracker.ietf.org/doc/html/rfc7235#section-3.1",
            Title = "Unauthorized",
            Detail = exception.Message
        };

        context.Response.StatusCode = problemDetails.Status.Value;
        await context.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
    }
}
