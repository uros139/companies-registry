using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace CompaniesRegistry.Api.Infrastructure.ExceptionHandling;

public class ValidationExceptionHandler(ILogger<ValidationExceptionHandler> logger) : IExceptionHandlerStrategy
{
    public bool CanHandle(Exception exception) => exception is ValidationException;

    public async ValueTask HandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
    {
        var validationException = (ValidationException)exception;

        logger.LogWarning(exception, "Validation failed");

        var problemDetails = new ValidationProblemDetails(
            validationException.Errors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(e => e.ErrorMessage).ToArray()
                ))
        {
            Status = StatusCodes.Status400BadRequest,
            Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1",
            Title = "One or more validation errors occurred."
        };

        context.Response.StatusCode = problemDetails.Status.Value;
        await context.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
    }
}
