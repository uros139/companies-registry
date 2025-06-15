using CompaniesRegistry.Domain.Companies;
using CompaniesRegistry.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace CompaniesRegistry.Application.Abstractions.Data;

public interface IApplicationDbContext
{
    DbSet<Company> Companies { get; }
    DbSet<User> Users { get; }

}
