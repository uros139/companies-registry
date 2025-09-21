using AutoMapper;
using CompaniesRegistry.Application.Features.Companies.Mapping;
using Microsoft.Extensions.Logging.Abstractions;

namespace CompaniesRegistry.Application.Tests.Mapping;

[Trait("Category", "Unit")]
public class AutoMapperProfileTests
{
    [Fact]
    public void All_Profiles_Should_Be_Valid()
    {
        var appAssembly = typeof(CompanyMappingProfile).Assembly;
        var config = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(appAssembly);
            },
            NullLoggerFactory.Instance);

        config.AssertConfigurationIsValid();
    }
}
