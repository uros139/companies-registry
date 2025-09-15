using CompaniesRegistry.Domain.Companies;
using CompaniesRegistry.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace CompaniesRegistry.Infrastructure.Tests.Database;

[Trait("Category", "Unit")]
public class ApplicationDbContextTests
{
    [Fact]
    public void Can_Insert_And_Retrieve_Company()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("InfraTestDb")
            .Options;

        using (var context = new ApplicationDbContext(options))
        {
            var company = new Company
            {
                Id = Guid.NewGuid(),
                Name = "Test Corp",
                Exchange = "NASDAQ",
                Ticker = "TST",
                Isin = "US1234567890",
                WebSite = "https://test.com"
            };
            context.Companies.Add(company);
            context.SaveChanges();
        }

        using (var context = new ApplicationDbContext(options))
        {
            var company = context.Companies.Single();
            Assert.Equal("Test Corp", company.Name);
            Assert.Equal("NASDAQ", company.Exchange);
        }
    }
}
