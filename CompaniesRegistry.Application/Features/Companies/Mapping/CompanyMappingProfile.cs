using AutoMapper;
using CompaniesRegistry.Application.Features.Companies.Get;
using CompaniesRegistry.Domain.Companies;

namespace CompaniesRegistry.Application.Features.Companies.Mapping;

public class CompanyMappingProfile : Profile
{
    public CompanyMappingProfile()
    {
        CreateMap<Company, CompanyResponse>();
    }
}
