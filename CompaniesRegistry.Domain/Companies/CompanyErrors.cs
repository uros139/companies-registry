using CompaniesRegistry.SharedKernel;

namespace CompaniesRegistry.Domain.Companies;

public static class CompanyErrors
{
    public static Error NotFound(Guid todoItemId) => Error.NotFound(
        "Companies.NotFound",
        $"The to-do item with the Id = '{todoItemId}' was not found");
}
