using CompaniesRegistry.Application.Abstractions.Messaging;

namespace CompaniesRegistry.Application.Features.Users.GetById;

public sealed record GetUserByIdQuery(Guid UserId) : IQuery<UserResponse>;
