using CompaniesRegistry.Application.Abstractions.Data;
using CompaniesRegistry.Domain.Users;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CompaniesRegistry.Application.Features.Users.Register;

internal sealed class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator(IRepository<User> repository)
    {
        RuleFor(c => c.FirstName).NotEmpty();
        RuleFor(c => c.LastName).NotEmpty();
        RuleFor(c => c.Password).NotEmpty().MinimumLength(8);
        RuleFor(c => c.Email)
            .NotEmpty()
            .EmailAddress()
            .MustAsync(async (email, ct) =>
                !await repository.QueryAllAsNoTracking().AnyAsync(u => u.Email == email, ct))
            .WithMessage("A user with this email already exists.");
    }
}
