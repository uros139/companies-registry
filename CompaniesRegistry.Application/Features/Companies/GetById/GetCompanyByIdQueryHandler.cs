using AutoMapper;
using AutoMapper.QueryableExtensions;
using CompaniesRegistry.Application.Abstractions.Data;
using CompaniesRegistry.Domain.Companies;
using CompaniesRegistry.SharedKernel.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CompaniesRegistry.Application.Features.Companies.GetById;

internal sealed class GetCompanyByIdQueryHandler(
    IRepository<Company> companiesRepository,
    IMapper mapper,
    ILogger<GetCompanyByIdQueryHandler> logger
    ) : IRequestHandler<GetCompanyByIdQuery, CompanyResponse>
{
    public async Task<CompanyResponse> Handle(GetCompanyByIdQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("Fetching company with ID {CompanyId}", query.Id);

        var company = await companiesRepository
            .QueryAllAsNoTracking()
            .Where(c => c.Id == query.Id)
            .ProjectTo<CompanyResponse>(mapper.ConfigurationProvider)
            .SingleOrDefaultAsync(cancellationToken) ?? throw new NotFoundException($"Company with ID {query.Id} not found.");

        return company;
    }
}
