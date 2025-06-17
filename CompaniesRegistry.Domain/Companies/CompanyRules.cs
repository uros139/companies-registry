namespace CompaniesRegistry.Domain.Companies;

public static class CompanyRules
{
    public static bool IsIsinFormatValid(string isin)
    {
        return !String.IsNullOrWhiteSpace(isin)
            && isin.Length >= 2
            && Char.IsLetter(isin[0])
            && Char.IsLetter(isin[1]);
    }

    public static bool IsWebsiteUrlValid(string? url)
    {
        return String.IsNullOrWhiteSpace(url) || Uri.TryCreate(url, UriKind.Absolute, out _);
    }
}
