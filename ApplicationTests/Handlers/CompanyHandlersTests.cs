using AutoMapper;
using CompaniesRegistry.Application.Abstractions.Data;
using CompaniesRegistry.Application.Features.Companies.Create;
using CompaniesRegistry.Application.Features.Companies.GetById;
using CompaniesRegistry.Application.Features.Companies.Mapping;
using CompaniesRegistry.Application.Features.Companies.Update;
using CompaniesRegistry.Domain.Companies;
using CompaniesRegistry.SharedKernel.Exceptions;
using Microsoft.Extensions.Logging.Abstractions;
using MockQueryable;
using Moq;

namespace ApplicationTests.Handlers;

public class CompanyHandlersTests
{
    private readonly Mock<IRepository<Company>> _companyRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly CreateCompanyCommandHandler _createHandler;

    public CompanyHandlersTests()
    {
        _companyRepositoryMock = new Mock<IRepository<Company>>();
        _mapperMock = new Mock<IMapper>();
        _createHandler = new CreateCompanyCommandHandler(_companyRepositoryMock.Object, _mapperMock.Object);
    }

    private (CreateCompanyCommand command, Company company, CompanyResponse response)
        SetupCreateCommandData(string name, string exchange, string ticker, string isin, string website)
    {
        var command = new CreateCompanyCommand
        {
            Name = name,
            Exchange = exchange,
            Ticker = ticker,
            Isin = isin,
            WebSite = website
        };

        var company = new Company
        {
            Name = command.Name,
            Exchange = command.Exchange,
            Ticker = command.Ticker,
            Isin = command.Isin,
            WebSite = command.WebSite
        };

        var response = new CompanyResponse
        {
            Id = Guid.NewGuid(),
            Name = command.Name,
            Exchange = command.Exchange,
            Ticker = command.Ticker,
            Isin = command.Isin,
            WebSite = command.WebSite
        };

        _mapperMock.Setup(m => m.Map<Company>(command)).Returns(company);
        _companyRepositoryMock.Setup(r => r.AddAsync(company, It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);
        _companyRepositoryMock.Setup(r => r.SaveChangesAsync(It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);
        _mapperMock.Setup(m => m.Map<CompanyResponse>(company)).Returns(response);

        return (command, company, response);
    }

    [Fact]
    public async Task Create_ShouldAddCompanyAndReturnResponse()
    {
        // Arrange
        var (command, company, response) = SetupCreateCommandData("Test Company", "NASDAQ", "TST", "US1234567890", "https://test.com");

        // Act
        var result = await _createHandler.Handle(command, CancellationToken.None);

        // Assert
        _mapperMock.Verify(m => m.Map<Company>(command), Times.Once);
        _companyRepositoryMock.Verify(r => r.AddAsync(company, It.IsAny<CancellationToken>()), Times.Once);
        _companyRepositoryMock.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        _mapperMock.Verify(m => m.Map<CompanyResponse>(company), Times.Once);

        Assert.Equal(response, result);
    }

    [Fact]
    public async Task Create_ShouldCallRepositoryAddAndSaveChanges()
    {
        // Arrange
        var (command, company, response) = SetupCreateCommandData("Sample Company", "NYSE", "SMP", "US9876543210", "https://sample.com");

        // Act
        var result = await _createHandler.Handle(command, CancellationToken.None);

        // Assert
        _companyRepositoryMock.Verify(r => r.AddAsync(company, It.IsAny<CancellationToken>()), Times.Once);
        _companyRepositoryMock.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        Assert.Equal(response, result);
    }

    [Fact]
    public async Task GetCompanyById_ShouldThrowNotFoundException_WhenCompanyNotFound()
    {
        // Arrange
        var query = new GetCompanyByIdQuery(Guid.NewGuid());
        var companyRepositoryMock = new Mock<IRepository<Company>>();
        var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile<CompanyMappingProfile>());
        var mapperMock = new Mock<IMapper>();
        mapperMock.Setup(m => m.ConfigurationProvider).Returns(mapperConfig);
        var loggerMock = new NullLogger<GetCompanyByIdQueryHandler>();

        var companiesData = new List<Company>().AsQueryable().BuildMock();

        companyRepositoryMock.Setup(r => r.QueryAllAsNoTracking()).Returns(companiesData);

        var handler = new GetCompanyByIdQueryHandler(
            companyRepositoryMock.Object,
            mapperMock.Object,
            loggerMock
        );

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(query, CancellationToken.None));

        companyRepositoryMock.Verify(r => r.QueryAllAsNoTracking(), Times.Once);
    }


    [Fact]
    public async Task GetCompanyById_ShouldCallRepositoryQueryAllAsNoTracking()
    {
        // Arrange
        var companyId = Guid.NewGuid();
        var query = new GetCompanyByIdQuery(companyId);

        var company = new Company { Id = companyId, Name = "TestCo" /* set required properties */ };
        var companiesMock = new List<Company> { company }.AsQueryable().BuildMock();

        var companyRepositoryMock = new Mock<IRepository<Company>>();
        companyRepositoryMock.Setup(r => r.QueryAllAsNoTracking()).Returns(companiesMock);

        var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile<CompanyMappingProfile>());
        var mapperMock = new Mock<IMapper>();
        mapperMock.Setup(m => m.ConfigurationProvider).Returns(mapperConfig);

        var loggerMock = new NullLogger<GetCompanyByIdQueryHandler>();

        var handler = new GetCompanyByIdQueryHandler(
            companyRepositoryMock.Object,
            mapperMock.Object,
            loggerMock
        );

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        companyRepositoryMock.Verify(r => r.QueryAllAsNoTracking(), Times.Once);
        Assert.NotNull(result); // Optional, to check something is returned
    }

    [Fact]
    public async Task UpdateCompany_ShouldUpdateCompanyAndReturnResponse_WhenCompanyExists()
    {
        var companyId = Guid.NewGuid();

        var existingCompany = new Company
        {
            Id = companyId,
            Name = "Old Name",
            Exchange = "Old Exchange",
            Ticker = "OldTicker",
            Isin = "OldIsin",
            WebSite = "https://old.website"
        };

        var companies = new List<Company> { existingCompany }.AsQueryable().BuildMock();

        var companyRepositoryMock = new Mock<IRepository<Company>>();
        companyRepositoryMock.Setup(r => r.QueryAll()).Returns(companies);
        companyRepositoryMock.Setup(r => r.SaveChangesAsync(It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

        var updateCommand = new UpdateCompanyCommand
        {
            Id = companyId,
            Name = "Updated Name",
            Exchange = "Updated Exchange",
            Ticker = "UpdatedTicker",
            Isin = "UpdatedIsin",
            WebSite = "https://updated.website"
        };

        var mapperMock = new Mock<IMapper>();
        mapperMock.Setup(m => m.Map<CompanyResponse>(It.IsAny<Company>()))
            .Returns<Company>(c => new CompanyResponse
            {
                Id = c.Id,
                Name = c.Name,
                Exchange = c.Exchange,
                Ticker = c.Ticker,
                Isin = c.Isin,
                WebSite = c.WebSite
            });

        var handler = new UpdateCompanyCommandHandler(
            companyRepositoryMock.Object,
            mapperMock.Object);

        var result = await handler.Handle(updateCommand, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(updateCommand.Name, existingCompany.Name);
        Assert.Equal(updateCommand.Exchange, existingCompany.Exchange);
        Assert.Equal(updateCommand.Ticker, existingCompany.Ticker);
        Assert.Equal(updateCommand.Isin, existingCompany.Isin);
        Assert.Equal(updateCommand.WebSite, existingCompany.WebSite);

        Assert.Equal(existingCompany.Id, result.Id);
        Assert.Equal(existingCompany.Name, result.Name);

        companyRepositoryMock.Verify(r => r.QueryAll(), Times.Once);
        companyRepositoryMock.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        mapperMock.Verify(m => m.Map<CompanyResponse>(existingCompany), Times.Once);
    }
}
