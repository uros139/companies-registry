using CompaniesRegistry.SharedKernel;

namespace CompaniesRegistry.Domain.Companies;

public class Company : Entity
{
    public string Name { get; set; } = String.Empty;
    public string Exchange { get; set; } = String.Empty;
    public string Ticker { get; set; } = String.Empty;
    public string Isin { get; set; } = String.Empty;
    public string? WebSite { get; set; }

    public void Update(CompanyUpdateDto dto)
    {
        Name = dto.Name;
        Exchange = dto.Exchange;
        Ticker = dto.Ticker;
        Isin = dto.Isin;
        WebSite = dto.WebSite;
    }
}
