using MarketWizard.Application.Contracts.Persistence;
using MarketWizard.Domain.Entities;

namespace MarketWizard.Data.Repositories;

public class AssetRepository(MarketWizardContext context)
    : GenericRepository<Asset>(context), IAssetRepository
{
}

