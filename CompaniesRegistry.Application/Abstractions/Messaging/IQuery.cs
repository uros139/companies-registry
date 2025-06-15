using MediatR;

namespace CompaniesRegistry.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<TResponse>;
