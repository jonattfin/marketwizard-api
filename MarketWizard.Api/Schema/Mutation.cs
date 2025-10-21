using MarketWizard.Application.Features.Portfolios.AddPortfolio;
using MarketWizard.Application.Features.Portfolios.DeletePortfolio;
using MarketWizard.Application.Features.Portfolios.UpdatePortfolio;
using MarketWizard.Application.Features.Watchlist.AddAsset;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MarketWizardApi.Schema;

public class Mutation
{
    public async Task<AddPortfolioOutputDto> AddPortfolio(AddPortfolioInputDto portfolioInput, [FromServices] IMediator mediator, CancellationToken cancellationToken)
    {
        return await mediator.Send(new AddPortfolioCommand(portfolioInput), cancellationToken);
    }
    
    public async Task<UpdatePortfolioOutputDto> UpdatePortfolio(UpdatePortfolioInputDto portfolioInput, [FromServices] IMediator mediator, CancellationToken cancellationToken)
    {
        return await mediator.Send(new UpdatePortfolioCommand(portfolioInput), cancellationToken);
    }
    
    public async Task<bool> DeletePortfolio(Guid portfolioId, [FromServices] IMediator mediator, CancellationToken cancellationToken)
    {
        return await mediator.Send(new DeletePortfolioCommand(portfolioId), cancellationToken);
    }
    
    public async Task<AddAssetOutputDto> AddAssetToWatchlist(AddAssetInputDto assetInput, [FromServices] IMediator mediator, CancellationToken cancellationToken)
    {
        return await mediator.Send(new AddAssetCommand(assetInput), cancellationToken);
    }
}