using FluentValidation;

namespace MarketWizard.Application.Features.Portfolios.AddPortfolio;

public class AddPortfolioCommandValidator: AbstractValidator<AddPortfolioCommand>
{
    public AddPortfolioCommandValidator()
    {
        RuleFor(x => x.AddPortfolio).NotNull();
        RuleFor(x => x.AddPortfolio.Name).NotEmpty().MinimumLength(3);
        RuleFor(x => x.AddPortfolio.Description).NotEmpty().MinimumLength(10);
    }
}