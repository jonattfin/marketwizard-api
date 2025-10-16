using MarketWizard.Application.Contracts.Persistence;
using MarketWizard.Application.Features.Portfolios.AddPortfolio;
using MarketWizard.Domain.Entities;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace MarketWizard.Data.Repositories;

public class PortfolioRepository(MarketWizardContext context, IPublishEndpoint publishEndpoint) : GenericRepository<Portfolio>(context), IPortfolioRepository
{
    public override async Task Insert(Portfolio entity, CancellationToken cancellationToken = default)
    {
        await base.Insert(entity, cancellationToken);
        
        await publishEndpoint.Publish(new PortfolioAddedEvent() { PortfolioId = entity.Id }, cancellationToken);
    }

    public IQueryable<Portfolio> GetAllWithRelatedEntities(CancellationToken cancellationToken = default)
    {
        return DbSet
            .Include(p => p.PortfolioAssets)
            .ThenInclude(pa => pa.Asset)
            .ThenInclude(pa => pa.PriceHistories);
    }

    public async Task<Portfolio?> GetByIdWithRelatedEntities(object id, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Include(p => p.PortfolioAssets)
            .ThenInclude(pa => pa.Asset)
            .ThenInclude(pa => pa.PriceHistories)
            .FirstOrDefaultAsync(p => p.Id == (Guid)id, cancellationToken);
    }
}