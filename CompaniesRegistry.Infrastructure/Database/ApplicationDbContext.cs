using CompaniesRegistry.Application.Abstractions.Data;
using CompaniesRegistry.Domain.Companies;
using Microsoft.EntityFrameworkCore;

namespace CompaniesRegistry.Infrastructure.Database;

public sealed class ApplicationDbContext(
    DbContextOptions<ApplicationDbContext> options)
    : DbContext(options), IApplicationDbContext
{
    public DbSet<Company> Companies { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}
