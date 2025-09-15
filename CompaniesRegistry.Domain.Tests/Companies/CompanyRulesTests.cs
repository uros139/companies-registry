using CompaniesRegistry.Domain.Companies;
using FluentAssertions;

namespace CompaniesRegistry.Domain.Tests.Companies;

[Trait("Category", "Unit")]
public class CompanyRulesTests
{
    [Theory]
    [InlineData("US1234567890", true)]
    [InlineData("GB0002634946", true)]
    [InlineData("1234567890", false)]
    [InlineData("", false)]
    [InlineData("U", false)]
    public void IsIsinFormatValid_Should_Work_As_Expected(string isin, bool expected)
    {
        CompanyRules.IsIsinFormatValid(isin).Should().Be(expected);
    }

    [Theory]
    [InlineData("https://example.com", true)]
    [InlineData("http://example.com", true)]
    [InlineData("ftp://example.com", true)]
    [InlineData("", true)]
    [InlineData(null, true)]
    [InlineData("not-a-url", false)]
    [InlineData("example.com", false)]
    public void IsWebsiteUrlValid_Should_Work_As_Expected(string? url, bool expected)
    {
        CompanyRules.IsWebsiteUrlValid(url).Should().Be(expected);
    }
}
