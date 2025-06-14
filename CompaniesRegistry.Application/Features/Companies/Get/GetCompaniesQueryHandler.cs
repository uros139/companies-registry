using AutoMapper;
using AutoMapper.QueryableExtensions;
using CompaniesRegistry.Application.Abstractions.Data;
using CompaniesRegistry.Application.Features.Companies.GetById;
using CompaniesRegistry.Domain.Companies;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CompaniesRegistry.Application.Features.Companies.Get;

internal sealed class GetCompaniesQueryHandler(
    IRepository<Company> companiesRepository,
    IMapper mapper
    ) : IRequestHandler<GetCompaniesQuery, List<CompanyResponse>>
{
    public async Task<List<CompanyResponse>> Handle(GetCompaniesQuery request, CancellationToken cancellationToken)
    {
        return await companiesRepository
            .QueryAllAsNoTracking()
            .ProjectTo<CompanyResponse>(mapper.ConfigurationProvider, cancellationToken)
            .ToListAsync(cancellationToken);
    }
}
