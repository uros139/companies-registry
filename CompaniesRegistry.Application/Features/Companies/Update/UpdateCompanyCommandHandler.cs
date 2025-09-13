using AutoMapper;
using CompaniesRegistry.Application.Abstractions.Data;
using CompaniesRegistry.Application.Features.Companies.GetById;
using CompaniesRegistry.Domain.Companies;
using CompaniesRegistry.SharedKernel.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CompaniesRegistry.Application.Features.Companies.Update;

internal sealed class UpdateCompanyCommandHandler(
    IRepository<Company> companyRepository,
    IMapper mapper
    ) : IRequestHandler<UpdateCompanyCommand, CompanyResponse>
{
    public async Task<CompanyResponse> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
    {
        var company = await companyRepository
            .QueryAll()
            .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken) ??
                throw new NotFoundException($"Company with Id '{request.Id}' not found.");

        var updateDto = new CompanyUpdateDto(
            request.Name,
            request.Exchange,
            request.Ticker,
            request.Isin,
            request.WebSite);

        company.Update(updateDto);

        return mapper.Map<CompanyResponse>(company);
    }
}
