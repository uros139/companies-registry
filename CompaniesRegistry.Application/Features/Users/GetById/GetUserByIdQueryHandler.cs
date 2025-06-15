using CompaniesRegistry.Application.Abstractions.Authentication;
using CompaniesRegistry.Application.Abstractions.Data;
using CompaniesRegistry.Domain.Users;
using CompaniesRegistry.SharedKernel.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CompaniesRegistry.Application.Features.Users.GetById;

internal sealed class GetUserByIdQueryHandler(IRepository<User> usersRepository, IUserContext userContext)
    : IRequestHandler<GetUserByIdQuery, UserResponse>
{
    public async Task<UserResponse> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
    {
        if (query.UserId != userContext.UserId)
        {
            throw new UnauthorizedAccessException("You are not authorized to access this user.");
        }

        UserResponse? user = await usersRepository
            .QueryAllAsNoTracking()
            .Where(u => u.Id == query.UserId)
            .Select(u => new UserResponse
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email
            })
            .SingleOrDefaultAsync(cancellationToken);

        if (user is null)
        {
            throw new NotFoundException($"User with ID {query.UserId} was not found.");
        }

        return user;
    }
}
