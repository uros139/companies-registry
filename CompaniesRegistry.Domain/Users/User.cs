using CompaniesRegistry.SharedKernel;

namespace CompaniesRegistry.Domain.Users;

public sealed class User : Entity
{
    public string Email { get; set; } = String.Empty;
    public string FirstName { get; set; } = String.Empty;
    public string LastName { get; set; } = String.Empty;
    public string PasswordHash { get; set; } = String.Empty;
}
