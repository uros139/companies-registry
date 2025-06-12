using MediatR;

namespace CompaniesRegistry.Application.Abstractions.Messaging;

public interface IQuery<out TResponse> : IRequest<TResponse>;