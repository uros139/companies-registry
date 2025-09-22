using CompaniesRegistry.Domain.Companies;
using Microsoft.EntityFrameworkCore;

namespace CompaniesRegistry.Infrastructure.Persistence.Seeding;

internal static class CompanySeed
{
    private static readonly List<Company> Companies = new()
    {
        new Company
        {
            Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
            Name = "Contoso Ltd.",
            Exchange = "NASDAQ",
            Ticker = "CTSO",
            Isin = "US1234567890",
            WebSite = "https://contoso.com"
        },
        new Company
        {
            Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
            Name = "Fabrikam Inc.",
            Exchange = "NYSE",
            Ticker = "FBKM",
            Isin = "US0987654321",
            WebSite = "https://fabrikam.com"
        },
        new Company
        {
            Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
            Name = "Northwind Traders",
            Exchange = "LSE",
            Ticker = "NWT",
            Isin = "GB1234567890",
            WebSite = "https://northwindtraders.com"
        },
        new Company
        {
            Id = Guid.Parse("44444444-4444-4444-4444-444444444444"),
            Name = "Adventure Works",
            Exchange = "TSX",
            Ticker = "ADVW",
            Isin = "CA1234567890",
            WebSite = "https://adventure-works.com"
        }
    };

    public static void Seed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Company>().HasData(Companies);
    }
}
