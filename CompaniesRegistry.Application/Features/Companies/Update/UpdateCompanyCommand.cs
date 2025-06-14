using System.Text.Json.Serialization;
using CompaniesRegistry.Application.Abstractions.Messaging;
using CompaniesRegistry.Application.Features.Companies.GetById;

namespace CompaniesRegistry.Application.Features.Companies.Update;

public sealed record UpdateCompanyCommand : ICommand<CompanyResponse>
{
    [JsonIgnore]
    public Guid Id { get; set; }
    public string Name { get; set; } = String.Empty;
    public string Exchange { get; set; } = String.Empty;
    public string Ticker { get; set; } = String.Empty;
    public string Isin { get; set; } = String.Empty;
    public string? WebSite { get; set; }
}
