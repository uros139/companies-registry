using CompaniesRegistry.SharedKernel;
using MediatR;

namespace CompaniesRegistry.Application.Abstractions.Messaging;

public interface ICommand;

public interface ICommand<TResponse> : IRequest<Result<TResponse>> { }
