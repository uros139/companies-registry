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
        var query = companiesRepository.QueryAllAsNoTracking();

        if (!String.IsNullOrWhiteSpace(request.Isin))
        {
            query = query.Where(c => c.Isin.StartsWith(request.Isin));
        }

        return await query
            .ProjectTo<CompanyResponse>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}
