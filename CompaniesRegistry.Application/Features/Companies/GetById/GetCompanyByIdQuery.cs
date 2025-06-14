using CompaniesRegistry.Application.Abstractions.Messaging;

namespace CompaniesRegistry.Application.Features.Companies.GetById;

public sealed record GetCompanyByIdQuery(Guid Id) : IQuery<CompanyResponse> { }
