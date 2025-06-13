using CompaniesRegistry.Application.Abstractions.Messaging;

namespace CompaniesRegistry.Application.Features.Companies.Get;

public sealed record GetCompaniesQuery : IQuery<List<CompanyResponse>>;
