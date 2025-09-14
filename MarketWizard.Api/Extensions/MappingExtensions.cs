using Mapster;
using MarketWizard.Application.Dto;
using MarketWizard.Domain.Entities;

namespace MarketWizardApi.Extensions;

public static class MappingExtensions
{
    public static IEnumerable<AssetDto> ToAssetDtos(this IEnumerable<Asset> assets)
    {
        return assets.Adapt<IEnumerable<AssetDto>>();
    }
    
    public static IQueryable<PortfolioSummaryDto> ToSummaryDtos(this IQueryable<Portfolio> portfolios)
    {
        return portfolios.Select(portfolio => new PortfolioSummaryDto()
        {
            Id = portfolio.Id,
            Name = portfolio.Name,
            Description = portfolio.Description,
            ImageUrl = portfolio.ImageUrl,
            Holdings = portfolio.PortfolioAssets.Count,
            TotalValue = portfolio.TotalValue,
            UnrealizedGain = portfolio.UnrealizedGain,
            CreatedAt = portfolio.CreatedAt,
        });
    }
    
     public static PortfolioDetailsDto? ToDetailsDto(this Portfolio? portfolio)
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