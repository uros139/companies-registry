namespace CompaniesRegistry.SharedKernel.Exceptions;

public class NotFoundException(string message) : Exception(message)
{
}
