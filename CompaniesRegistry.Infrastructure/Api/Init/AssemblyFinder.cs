using System.Reflection;

namespace CompaniesRegistry.Infrastructure.Api.Init;

public static class AssemblyFinder
{
    private const string ProjectPrefix = "CompaniesRegistry";

    public static Assembly ApplicationAssembly => FindAssembly("Application");
    public static Assembly DomainAssembly => FindAssembly("Domain");
    public static Assembly InfrastructureAssembly => FindAssembly("Infrastructure");

    private static Assembly FindAssembly(string projectSuffix) => Find($"{ProjectPrefix}.{projectSuffix}");

    public static Assembly Find(string assemblyName) => Assembly.Load(assemblyName);
}
