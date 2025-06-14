using FluentValidation;

namespace CompaniesRegistry.Application.Features.Companies.Create;

public class CreateCompanyCommandValidator : AbstractValidator<CreateCompanyCommand>
{
    public CreateCompanyCommandValidator()
    {
        RuleFor(c => c.Isin)
            .NotEmpty()
            .Must(isin =>
                isin.Length >= 2 &&
                Char.IsLetter(isin[0]) &&
                Char.IsLetter(isin[1]))
            .WithMessage("The first two characters of ISIN must be letters.");
    }
}

