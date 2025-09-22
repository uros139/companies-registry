using CompaniesRegistry.Application.Abstractions.Data;
using CompaniesRegistry.Domain.Companies;
using CompaniesRegistry.Domain.Users;
using CompaniesRegistry.Infrastructure.Persistance.Seeding;
using Microsoft.EntityFrameworkCore;

namespace CompaniesRegistry.Infrastructure.Persistance;

public sealed class ApplicationDbContext(
    DbContextOptions<ApplicationDbContext> options)
    : DbContext(options), IApplicationDbContext
{
    public DbSet<Company> Companies { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
        CompanySeed.Seed(modelBuilder);
    }
}
