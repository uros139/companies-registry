using MediatR;

namespace CompaniesRegistry.Core.Cqrs;

public interface IQuery<out TResponse> : IRequest<TResponse>;