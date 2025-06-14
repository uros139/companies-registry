using AutoMapper;
using AutoMapper.QueryableExtensions;
using CompaniesRegistry.Application.Abstractions.Data;
using CompaniesRegistry.Application.Features.Companies.GetById;
using CompaniesRegistry.Domain.Companies;
using CompaniesRegistry.SharedKernel;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CompaniesRegistry.Application.Features.Companies.Get;

internal sealed class GetCompaniesQueryHandler(
    IRepository<Company> companiesRepository,
    IMapper mapper,
    ILogger<GetCompaniesQueryHandler> logger
    ) : IRequestHandler<GetCompaniesQuery, Result<List<CompanyResponse>>>
{
    public async Task<Result<List<CompanyResponse>>> Handle(GetCompaniesQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Handling {Query} at {Time}", nameof(GetCompaniesQuery), DateTime.UtcNow);

        return await companiesRepository
            .QueryAllAsNoTracking()
            .ProjectTo<CompanyResponse>(mapper.ConfigurationProvider, cancellationToken)
            .ToListAsync(cancellationToken);
    }
}
