using CompaniesRegistry.Application.Abstractions.Data;
using CompaniesRegistry.Domain;
using CompaniesRegistry.Domain.Companies;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CompaniesRegistry.Application.Features.Companies.Update;

public class UpdateCompanyCommandValidator : AbstractValidator<UpdateCompanyCommand>
{
    private readonly IRepository<Company> _repository;

    public UpdateCompanyCommandValidator(IRepository<Company> repository)
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
            .Must(isin =>
                isin.Length >= 2 &&
                Char.IsLetter(isin[0]) &&
                Char.IsLetter(isin[1]))
            .WithMessage("The first two characters of ISIN must be letters.");
        RuleFor(c => c.Isin)
            .MustAsync(async (isin, cancellationToken) =>
                !await IsDuplicateIsinAsync(isin, cancellationToken))
            .WithMessage("A company with the same ISIN already exists.");
        RuleFor(c => c.WebSite)
            .Must(BeAValidUrl)
            .When(c => !String.IsNullOrWhiteSpace(c.WebSite))
            .WithMessage("Website must be a valid URL.");
    }

    private bool BeAValidUrl(string? url) =>
        Uri.TryCreate(url, UriKind.Absolute, out var uriResult);

    private async Task<bool> IsDuplicateIsinAsync(string isin, CancellationToken cancellationToken) =>
        await _repository
            .QueryAllAsNoTracking()
            .Where(c => c.Isin != isin)
            .AnyAsync(c => c.Isin == isin, cancellationToken);
}
