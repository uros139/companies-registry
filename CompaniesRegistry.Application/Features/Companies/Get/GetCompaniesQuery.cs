using CompaniesRegistry.Application.Abstractions.Messaging;
using CompaniesRegistry.Application.Features.Companies.GetById;

namespace CompaniesRegistry.Application.Features.Companies.Get;

public sealed record GetCompaniesQuery : IQuery<List<CompanyResponse>>;
