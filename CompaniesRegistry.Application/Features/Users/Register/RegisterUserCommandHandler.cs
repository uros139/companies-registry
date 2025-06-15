using AutoMapper;
using CompaniesRegistry.Application.Abstractions.Authentication;
using CompaniesRegistry.Application.Abstractions.Data;
using CompaniesRegistry.Application.Features.Users.GetById;
using CompaniesRegistry.Domain.Users;
using MediatR;

namespace CompaniesRegistry.Application.Features.Users.Register;

internal sealed class RegisterUserCommandHandler(
    IRepository<User> repository,
    IPasswordHasher passwordHasher,
    IMapper mapper
    )
    : IRequestHandler<RegisterUserCommand, UserResponse>
{
    public async Task<UserResponse> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = command.Email,
            FirstName = command.FirstName,
            LastName = command.LastName,
            PasswordHash = passwordHasher.Hash(command.Password)
        };

        await repository.AddAsync(user, cancellationToken);

        await repository.SaveChangesAsync(cancellationToken);

        return mapper.Map<UserResponse>(user);
    }
}
