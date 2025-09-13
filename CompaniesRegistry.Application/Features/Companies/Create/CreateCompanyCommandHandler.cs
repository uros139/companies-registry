using AutoMapper;
using CompaniesRegistry.Application.Abstractions.Data;
using CompaniesRegistry.Application.Features.Companies.GetById;
using CompaniesRegistry.Domain.Companies;
using MediatR;

namespace CompaniesRegistry.Application.Features.Companies.Create;

internal sealed class CreateCompanyCommandHandler(
    IRepository<Company> companyRepository,
    IMapper mapper
    ) : IRequestHandler<CreateCompanyCommand, CompanyResponse>
{
    public async Task<CompanyResponse> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
    {
        var company = mapper.Map<Company>(request);

        await companyRepository.AddAsync(company, cancellationToken);

        return mapper.Map<CompanyResponse>(company);
    }
}
