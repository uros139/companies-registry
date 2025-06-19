using AutoMapper;
using CompaniesRegistry.Application.Features.Companies.Mapping;

namespace CompaniesRegistry.Application.Tests.Mapping;

public class AutoMapperProfileTests
{
    [Fact]
    public void All_Profiles_Should_Be_Valid()
    {
        var appAssembly = typeof(CompanyMappingProfile).Assembly;
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddMaps(appAssembly);
        });

        config.AssertConfigurationIsValid();
    }
}
