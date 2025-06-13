using CompaniesRegistry.Infrastructure.Api.Init;
using NetArchTest.Rules;
using Shouldly;

namespace ArchitectureTests.Layers;

public class LayerTests
{
    [Fact]
    public void Domain_Should_NotHaveDependencyOnApplication()
    {
        var result = Types.InAssembly(AssemblyFinder.DomainAssembly)
            .Should()
            .NotHaveDependencyOn("Application")
            .GetResult();

        result.IsSuccessful.ShouldBeTrue();
    }

    [Fact]
    public void DomainLayer_ShouldNotHaveDependencyOn_InfrastructureLayer()
    {
        var result = Types.InAssembly(AssemblyFinder.DomainAssembly)
            .Should()
            .NotHaveDependencyOn(nameof(AssemblyFinder.InfrastructureAssembly))
            .GetResult();

        result.IsSuccessful.ShouldBeTrue();
    }

    [Fact]
    public void DomainLayer_ShouldNotHaveDependencyOn_PresentationLayer()
    {
        var result = Types.InAssembly(AssemblyFinder.DomainAssembly)
            .Should()
            .NotHaveDependencyOn(nameof(AssemblyFinder.PresentationAssembly))
            .GetResult();

        result.IsSuccessful.ShouldBeTrue();
    }

    [Fact]
    public void ApplicationLayer_ShouldNotHaveDependencyOn_InfrastructureLayer()
    {
        var result = Types.InAssembly(AssemblyFinder.ApplicationAssembly)
            .Should()
            .NotHaveDependencyOn(nameof(AssemblyFinder.InfrastructureAssembly))
            .GetResult();

        result.IsSuccessful.ShouldBeTrue();
    }

    [Fact]
    public void ApplicationLayer_ShouldNotHaveDependencyOn_PresentationLayer()
    {
        var result = Types.InAssembly(AssemblyFinder.ApplicationAssembly)
            .Should()
            .NotHaveDependencyOn(nameof(AssemblyFinder.PresentationAssembly))
            .GetResult();

        result.IsSuccessful.ShouldBeTrue();
    }

    [Fact]
    public void InfrastructureLayer_ShouldNotHaveDependencyOn_PresentationLayer()
    {
        var result = Types.InAssembly(AssemblyFinder.InfrastructureAssembly)
            .Should()
            .NotHaveDependencyOn(nameof(AssemblyFinder.PresentationAssembly))
            .GetResult();

        result.IsSuccessful.ShouldBeTrue();
    }
}
