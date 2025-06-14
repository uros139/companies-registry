using CompaniesRegistry.Domain;
using FluentValidation;

namespace CompaniesRegistry.Application.Features.Companies.Create;

public class CreateCompanyCommandValidator : AbstractValidator<CreateCompanyCommand>
{
    public CreateCompanyCommandValidator()
    {
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
        RuleFor(c => c.WebSite)
            .Must(BeAValidUrl)
            .When(c => !String.IsNullOrWhiteSpace(c.WebSite))
            .WithMessage("Website must be a valid URL.");
    }

    private bool BeAValidUrl(string? url) =>
        Uri.TryCreate(url, UriKind.Absolute, out var uriResult);
}


