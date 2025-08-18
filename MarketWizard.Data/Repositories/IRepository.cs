using MarketWizard.Domain;

namespace MarketWizard.Data.Repositories;

public interface IRepository
{
    IQueryable<Portfolio> GetPortfolios(CancellationToken cancellationToken);
}