using FluentValidation;
using MarketWizard.Application.Contracts.Persistence;

namespace MarketWizard.Application.Features.UpdatePortfolio;

public class UpdatePortfolioCommandValidator : AbstractValidator<UpdatePortfolioCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdatePortfolioCommandValidator(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;

        RuleFor(x => x.UpdatePortfolio).NotNull();
        
        RuleFor(x => x.UpdatePortfolio.Id).NotEmpty()
            .MustAsync(PortfolioExists);
        
        RuleFor(x => x.UpdatePortfolio.Name).NotEmpty().MinimumLength(3);
        RuleFor(x => x.UpdatePortfolio.Description).NotEmpty().MinimumLength(10);
        
        RuleFor(x => x.UpdatePortfolio.UserId).NotEmpty()
            .MustAsync(UserExists);
    }

    private async Task<bool> PortfolioExists(Guid portfolioId, CancellationToken cancellationToken)
    {
        var portfolio = await _unitOfWork.PortfolioRepository.GetById(portfolioId, cancellationToken);
        return portfolio != null;
    }
    
    private async Task<bool> UserExists(Guid userId, CancellationToken cancellationToken)
    {
        var portfolio = await _unitOfWork.UserRepository.GetById(userId, cancellationToken);
        return portfolio != null;
    }
}