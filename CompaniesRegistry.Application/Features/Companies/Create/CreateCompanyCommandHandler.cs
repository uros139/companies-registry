using AutoMapper;
using CompaniesRegistry.Application.Abstractions.Data;
using CompaniesRegistry.Domain.Companies;
using CompaniesRegistry.SharedKernel;
using MediatR;

namespace CompaniesRegistry.Application.Features.Companies.Create;

internal sealed class CreateCompanyCommandHandler(
    IRepository<Company> companyRepository,
    IMapper mapper
    ) : IRequestHandler<CreateCompanyCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
    {
        var company = mapper.Map<Company>(request);

        await companyRepository.AddAsync(company, cancellationToken);

        await companyRepository.SaveChangesAsync(cancellationToken);

        return Result.Success(company.Id);
    }
}
