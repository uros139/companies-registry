using System.Net;
using System.Net.Http.Json;
using CompaniesRegistry.Api.IntegrationTests.Infrastructure;
using CompaniesRegistry.Application.Features.Companies.GetById;
using FluentAssertions;

namespace CompaniesRegistry.Api.IntegrationTests;

public class CompaniesControllerTests : IClassFixture<TestApplicationFactory>
{
    private readonly HttpClient _client;

    public CompaniesControllerTests(TestApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Post_Should_Create_Company()
    {
        var command = new
        {
            Name = "Tesla Inc.",
            Exchange = "NASDAQ",
            Ticker = "TSLA",
            Isin = "US88160R1014",
            WebSite = "https://www.tesla.com"
        };

        var response = await _client.PostAsJsonAsync("/api/companies", command);

        response.StatusCode.Should().Be(HttpStatusCode.Created);

        var created = await response.Content.ReadFromJsonAsync<CompanyResponse>();
        created.Should().NotBeNull();
        created!.Name.Should().Be(command.Name);
        created.Ticker.Should().Be(command.Ticker);
    }

    [Fact]
    public async Task GetById_Should_Return_Created_Company()
    {
        var command = new
        {
            Name = "Apple Inc.",
            Exchange = "NASDAQ",
            Ticker = "AAPL",
            Isin = "US0378331005",
            WebSite = "https://www.apple.com"
        };

        var createResponse = await _client.PostAsJsonAsync("/api/companies", command);
        var created = await createResponse.Content.ReadFromJsonAsync<CompanyResponse>();

        var getResponse = await _client.GetAsync($"/api/companies/{created!.Id}");

        getResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        var company = await getResponse.Content.ReadFromJsonAsync<CompanyResponse>();
        company!.Id.Should().Be(created.Id);
        company.Name.Should().Be(command.Name);
    }

    [Fact]
    public async Task Put_Should_Update_Company()
    {
        var create = new
        {
            Name = "Amazon",
            Exchange = "NASDAQ",
            Ticker = "AMZN",
            Isin = "US0231351067",
            WebSite = "https://www.amazon.com"
        };

        var createResponse = await _client.PostAsJsonAsync("/api/companies", create);
        var created = await createResponse.Content.ReadFromJsonAsync<CompanyResponse>();

        var update = new
        {
            Name = "Amazon Updated",
            Exchange = "NYSE",
            Ticker = "AMZU",
            Isin = "US0231351067",
            WebSite = "https://updated.amazon.com"
        };

        var putResponse = await _client.PutAsJsonAsync($"/api/companies/{created!.Id}", update);

        putResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        var updated = await putResponse.Content.ReadFromJsonAsync<CompanyResponse>();

        updated!.Name.Should().Be(update.Name);
        updated.Exchange.Should().Be(update.Exchange);
        updated.Ticker.Should().Be(update.Ticker);
    }

    [Fact]
    public async Task GetById_Should_Return_NotFound_When_NotExist()
    {
        var id = Guid.NewGuid();
        var response = await _client.GetAsync($"/api/companies/{id}");
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Put_Should_Return_NotFound_When_NotExist()
    {
        var update = new
        {
            Name = "Ghost Company",
            Exchange = "N/A",
            Ticker = "GHOST",
            Isin = "NA",
            WebSite = "https://ghost.com"
        };

        var response = await _client.PutAsJsonAsync($"/api/companies/{Guid.NewGuid()}", update);
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Post_Should_Return_BadRequest_When_Invalid()
    {
        var invalid = new { };
        var response = await _client.PostAsJsonAsync("/api/companies", invalid);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}
