using CompaniesRegistry.Application.Abstractions.Messaging;
using CompaniesRegistry.Application.Features.Users.GetById;

namespace CompaniesRegistry.Application.Features.Users.Register;

public sealed record RegisterUserCommand(string Email, string FirstName, string LastName, string Password)
    : ICommand<UserResponse>;
