using AutoMapper;
using CompaniesRegistry.Application.Features.Companies.Create;
using CompaniesRegistry.Application.Features.Companies.GetById;
using CompaniesRegistry.Domain.Companies;

namespace CompaniesRegistry.Application.Features.Companies.Mapping;

public class CompanyMappingProfile : Profile
{
    public CompanyMappingProfile()
    {
        CreateMap<Company, CompanyResponse>().ReverseMap();
        CreateMap<CreateCompanyCommand, Company>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());  // ignore Id here because DB generates it
    }
}
