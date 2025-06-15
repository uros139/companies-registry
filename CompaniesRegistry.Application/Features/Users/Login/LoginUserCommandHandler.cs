using CompaniesRegistry.Application.Abstractions.Authentication;
using CompaniesRegistry.Application.Abstractions.Data;
using CompaniesRegistry.Domain.Users;
using CompaniesRegistry.SharedKernel.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CompaniesRegistry.Application.Features.Users.Login;

internal sealed class LoginUserCommandHandler(
    IRepository<User> usersRepository,
    IPasswordHasher passwordHasher,
    ITokenProvider tokenProvider
) : IRequestHandler<LoginUserCommand, LoginResponse>
{
    public async Task<LoginResponse> Handle(LoginUserCommand command, CancellationToken cancellationToken)
    {
        var user = await usersRepository
            .QueryAllAsNoTracking()
            .SingleOrDefaultAsync(u => u.Email == command.Email, cancellationToken);

        if (user is null)
        {
            throw new NotFoundException("User with specified email doesn't exist");
        }

        var verified = passwordHasher.Verify(command.Password, user.PasswordHash);

        if (!verified)
        {
            throw new UnauthorizedAccessException("Invalid password.");
        }

        var token = tokenProvider.Create(user);

        return new LoginResponse { Token = token };
    }
}
