using CompaniesRegistry.Application.Abstractions.Messaging;
using MediatR;

namespace CompaniesRegistry.Infrastructure.Database;
internal sealed class UnitOfWorkBehavior<TRequest, TResponse>(
    ApplicationDbContext dbContext
) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var response = await next(cancellationToken);

        // Only save changes for commands
        if (request is ICommand<TResponse>)
        {
            await dbContext.SaveChangesAsync(cancellationToken);
        }

        return response;
    }
}
