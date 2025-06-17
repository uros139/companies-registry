using CompaniesRegistry.Application.Abstractions.Data;
using CompaniesRegistry.Domain;
using CompaniesRegistry.Domain.Companies;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CompaniesRegistry.Application.Features.Companies.Create;

public class CreateCompanyCommandValidator : AbstractValidator<CreateCompanyCommand>
{
    private readonly IRepository<Company> _repository;

    public CreateCompanyCommandValidator(IRepository<Company> repository)
    {
        _repository = repository;

        RuleFor(c => c.Name)
            .NotEmpty()
            .MaximumLength(ModelConstants.DefaultStringMaxLength);
        RuleFor(c => c.Ticker)
            .NotEmpty()
            .MaximumLength(ModelConstants.DefaultStringMaxLength);
        RuleFor(c => c.Exchange)
            .NotEmpty()
            .MaximumLength(ModelConstants.DefaultStringMaxLength);
        RuleFor(c => c.Isin)
            .NotEmpty()
            .Must(CompanyRules.IsIsinFormatValid)
            .WithMessage("The first two characters of ISIN must be letters.");
        RuleFor(c => c.Isin)
            .MustAsync(async (isin, cancellationToken) =>
                !await IsDuplicateIsinAsync(isin, cancellationToken))
            .WithMessage("A company with the same ISIN already exists.");
        RuleFor(c => c.WebSite)
            .Must(CompanyRules.IsWebsiteUrlValid)
            .WithMessage("Website must be a valid URL.");
    }

    private async Task<bool> IsDuplicateIsinAsync(string isin, CancellationToken cancellationToken) =>
        await _repository
            .QueryAllAsNoTracking()
            .AnyAsync(c => c.Isin == isin, cancellationToken);
}


