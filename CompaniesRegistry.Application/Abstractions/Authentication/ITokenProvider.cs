using CompaniesRegistry.Domain.Users;

namespace CompaniesRegistry.Application.Abstractions.Authentication;

public interface ITokenProvider
{
    string Create(User user);
}
