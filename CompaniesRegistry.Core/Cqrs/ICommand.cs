using MediatR;

namespace CompaniesRegistry.Core.Cqrs;

public interface ICommand<TResponse> : IRequest<TResponse>;
