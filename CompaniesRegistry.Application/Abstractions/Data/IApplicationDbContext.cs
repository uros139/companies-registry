using CompaniesRegistry.Domain.Companies;
using Microsoft.EntityFrameworkCore;

namespace CompaniesRegistry.Application.Abstractions.Data;

public interface IApplicationDbContext
{
    DbSet<Company> Companies { get; }
}
