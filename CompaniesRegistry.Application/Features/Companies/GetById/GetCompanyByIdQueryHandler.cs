using AutoMapper;
using AutoMapper.QueryableExtensions;
using CompaniesRegistry.Application.Abstractions.Data;
using CompaniesRegistry.Domain.Companies;
using CompaniesRegistry.SharedKernel;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CompaniesRegistry.Application.Features.Companies.GetById;

internal sealed class GetCompanyByIdQueryHandler(
    IRepository<Company> companiesRepository,
    IMapper mapper,
    ILogger<GetCompanyByIdQueryHandler> logger
    ) : IRequestHandler<GetCompanyByIdQuery, Result<CompanyResponse>>
{
    public async Task<Result<CompanyResponse>> Handle(GetCompanyByIdQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("Fetching company with ID {CompanyId}", query.Id);

        var company = await companiesRepository
            .QueryAllAsNoTracking()
            .Where(c => c.Id == query.Id)
            .ProjectTo<CompanyResponse>(mapper.ConfigurationProvider, cancellationToken)
            .SingleOrDefaultAsync(c => c.Id == query.Id, cancellationToken);

        if (company is null)
        {
            return Result.Failure<CompanyResponse>(CompanyErrors.NotFound(query.Id));
        }
        return Result.Success(company);
    }
}
