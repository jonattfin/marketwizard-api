using MarketWizard.Application.Contracts.Persistence;
using MarketWizard.Domain.Entities;
using MediatR;

namespace MarketWizard.Application.Features.Portfolios.GetPortfolioById;

public class GetPortfolioByIdQuery : IRequest<PortfolioDetailsDto?>
{
    public Guid PortfolioId { get; init; }
}

public class GetPortfolioByIdQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetPortfolioByIdQuery, PortfolioDetailsDto?>
{
    public async Task<PortfolioDetailsDto?> Handle(GetPortfolioByIdQuery request, CancellationToken cancellationToken)
    {
        var portfolio = await unitOfWork.PortfolioRepository.GetByIdWithRelatedEntities(request.PortfolioId, cancellationToken);
        
        return ToDetailsDto(portfolio);
    }
    
    private static PortfolioDetailsDto? ToDetailsDto(Portfolio? portfolio)
    {
        if (portfolio is null)
            return null;

        return new PortfolioDetailsDto()
        {
            Id = portfolio.Id,
            Name = portfolio.Name,
            Description = portfolio.Description,
            ImageUrl = portfolio.ImageUrl,
            Holdings = portfolio.PortfolioAssets.Count,
            TotalValue = portfolio.TotalValue,
            UnrealizedGain = portfolio.UnrealizedGain,
            CreatedAt = portfolio.CreatedAt,
            Assets = portfolio.PortfolioAssets.Select(asset => new PortfolioDetailsAssetDto()
            {
                NumberOfShares = asset.NumberOfShares,
                PricePerShare = asset.PricePerShare,
                Symbol = asset.Asset.Symbol,
                PriceHistory = asset.Asset.PriceHistories.Select(x => new AssetPriceHistoryDto()
                {
                    Date = x.Date,
                    Price = x.Price
                })
            })
        };
    }
}