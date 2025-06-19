using CompaniesRegistry.Domain.Companies;
using FluentAssertions;

namespace CompaniesRegistry.Domain.Tests.Companies;

public class CompanyTests
{
    [Fact]
    public void Update_Should_Update_Company_Properties()
    {
        // Arrange
        var company = new Company();
        var dto = new CompanyUpdateDto(
            Name: "UpdatedName",
            Exchange: "NYSE",
            Ticker: "UPD",
            Isin: "US1234567890",
            WebSite: "https://example.com"
        );

        // Act
        company.Update(dto);

        // Assert
        company.Name.Should().Be(dto.Name);
        company.Exchange.Should().Be(dto.Exchange);
        company.Ticker.Should().Be(dto.Ticker);
        company.Isin.Should().Be(dto.Isin);
        company.WebSite.Should().Be(dto.WebSite);
    }
}
