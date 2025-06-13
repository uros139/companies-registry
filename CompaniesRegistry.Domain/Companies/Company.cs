namespace CompaniesRegistry.Domain.Companies;

public class Company
{
    public Guid Id { get; set; }
    public string Name { get; set; } = String.Empty;
    public string Exchange { get; set; } = String.Empty;
    public string Ticker { get; set; } = String.Empty;
    public string Isin{ get; set; } = String.Empty;
    public string? WebSite{ get; set; }
}
