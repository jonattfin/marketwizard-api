using FluentValidation;

namespace MarketWizard.Application.Features.UpdatePortfolio;

public class UpdatePortfolioCommandValidator: AbstractValidator<UpdatePortfolioCommand>
{
    public UpdatePortfolioCommandValidator()
    {
        RuleFor(x => x.UpdatePortfolio).NotNull();
        RuleFor(x => x.UpdatePortfolio.Name).NotEmpty().MinimumLength(3);
        RuleFor(x => x.UpdatePortfolio.Description).NotEmpty().MinimumLength(10);
        RuleFor(x => x.UpdatePortfolio.UserId).NotEmpty();
    }
}