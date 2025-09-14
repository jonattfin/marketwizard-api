
namespace MarketWizard.Application.Dto;

public class PortfolioDetailsAssetDto {
    
    public string Symbol { get; set; }
    
    public double NumberOfShares { get; set; }
    
    public double PricePerShare { get; set; }
    
    public IEnumerable<AssetPriceHistoryDto> PriceHistory { get; set; } = new List<AssetPriceHistoryDto>();
}