using CompaniesRegistry.Application.Abstractions.Messaging;
using CompaniesRegistry.Application.Features.Companies.GetById;

namespace CompaniesRegistry.Application.Features.Companies.Get;

public class GetCompaniesQuery : IQuery<List<CompanyResponse>>
{
    public string? Isin { get; set; }
}
