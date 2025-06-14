namespace CompaniesRegistry.Domain.Companies;

public record CompanyUpdateDto(string Name, string Exchange, string Ticker, string Isin, string? WebSite);
