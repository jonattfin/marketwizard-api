namespace MarketWizard.Application.Dto;

public class PortfolioDetailsDto : PortfolioSummaryDto
{
    public IEnumerable<PortfolioDetailsAssetDto> Assets { get; set; } = new List<PortfolioDetailsAssetDto>();
}