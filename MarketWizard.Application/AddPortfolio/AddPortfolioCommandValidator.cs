using FluentValidation;

namespace MarketWizard.Application.AddPortfolio;

public class AddPortfolioCommandValidator: AbstractValidator<AddPortfolioCommand>
{
    public AddPortfolioCommandValidator()
    {
        RuleFor(x => x.Portfolio).NotNull();
        RuleFor(x => x.Portfolio.Name).NotEmpty().MinimumLength(3);
        RuleFor(x => x.Portfolio.Description).NotEmpty().MinimumLength(10);
        RuleFor(x => x.Portfolio.UserId).NotEmpty();
    }
}