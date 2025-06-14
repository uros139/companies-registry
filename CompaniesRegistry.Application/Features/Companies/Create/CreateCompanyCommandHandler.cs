using AutoMapper;
using CompaniesRegistry.Application.Abstractions.Data;
using CompaniesRegistry.Domain.Companies;
using MediatR;

namespace CompaniesRegistry.Application.Features.Companies.Create;

internal sealed class CreateCompanyCommandHandler(
    IRepository<Company> companyRepository,
    IMapper mapper
    ) : IRequestHandler<CreateCompanyCommand, Guid>
{
    public async Task<Guid> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
    {
        var company = mapper.Map<Company>(request);

        await companyRepository.AddAsync(company, cancellationToken);

        await companyRepository.SaveChangesAsync(cancellationToken);

        return company.Id;
    }
}
