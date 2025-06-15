using CompaniesRegistry.Application.Abstractions.Messaging;

namespace CompaniesRegistry.Application.Features.Users.Login;

public sealed record LoginUserCommand(string Email, string Password) : ICommand<LoginResponse>;
