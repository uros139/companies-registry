using CompaniesRegistry.Application.Abstractions.Messaging;

namespace CompaniesRegistry.Application.Features.Companies.Create;

public sealed record CreateCompanyCommand : ICommand<Guid>
{
    public string Name { get; set; } = String.Empty;
    public string Exchange { get; set; } = String.Empty;
    public string Ticker { get; set; } = String.Empty;
    public string Isin { get; set; } = String.Empty;
    public string? WebSite { get; set; }
}
