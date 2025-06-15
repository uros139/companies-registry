namespace CompaniesRegistry.Application.Features.Users.GetById;

public sealed record UserResponse
{
    public Guid Id { get; init; }

    public string Email { get; init; } = String.Empty;

    public string FirstName { get; init; } = String.Empty;

    public string LastName { get; init; } = String.Empty;
}
