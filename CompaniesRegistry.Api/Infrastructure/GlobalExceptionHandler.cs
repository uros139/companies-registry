using CompaniesRegistry.Api.Infrastructure.ExceptionHandling;
using CompaniesRegistry.SharedKernel.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace CompaniesRegistry.Api.Infrastructure;

public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly IEnumerable<IExceptionHandlerStrategy> _handlers;
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(IEnumerable<IExceptionHandlerStrategy> handlers, ILogger<GlobalExceptionHandler> logger)
    {
        _handlers = handlers;
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var handler = _handlers.FirstOrDefault(h => h.CanHandle(exception));
        if (handler != null)
        {
            await handler.HandleAsync(httpContext, exception, cancellationToken);
            return true;
        }

        // fallback to generic 500 handler
        await HandleGenericAsync(httpContext, exception, cancellationToken);
        return true;
    }

    private async Task HandleGenericAsync(HttpContext context, Exception ex, CancellationToken cancellationToken)
    {
        _logger.LogError(ex, "Unhandled exception occurred");

        var problemDetails = new ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1",
            Title = "An unexpected error occurred."
        };

        context.Response.StatusCode = problemDetails.Status.Value;
        context.Response.ContentType = "application/json";

        await context.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
    }
}
