using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CompaniesRegistry.Application.Behaviors;

public class LoggingBehavior<TRequest, TResponse>(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IBaseRequest
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var requestType = typeof(TRequest).FullName ?? String.Empty;
        using (logger.BeginScope(
                   new List<KeyValuePair<string, object>> { new("MediatRRequestType", requestType) }))
        {
            logger.LogInformation("Handling {RequestType}", requestType);
            try
            {
                var response = await next(cancellationToken);
                logger.LogInformation("Handled {RequestType}", requestType);
                return response;
            }
            catch (ValidationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unhandled exception for {RequestType}", requestType);
                throw;
            }
        }
    }
}
