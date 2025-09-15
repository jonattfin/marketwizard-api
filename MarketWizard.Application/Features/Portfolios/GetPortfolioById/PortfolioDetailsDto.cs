using MarketWizard.Application.Features.Portfolios.GetPortfolios;

namespace MarketWizard.Application.Features.Portfolios.GetPortfolioById;

public class PortfolioDetailsDto : PortfolioSummaryDto
{
    public IEnumerable<PortfolioDetailsAssetDto> Assets { get; set; } = new List<PortfolioDetailsAssetDto>();
}