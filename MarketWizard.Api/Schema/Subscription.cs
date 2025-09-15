using MarketWizard.Application.Dto;

namespace MarketWizardApi.Schema;

public class Subscription
{
    [Subscribe]
    [Topic("StocksPriceUpdated")]
    public ICollection<StockQuoteDto> OnStockPriceUpdated([EventMessage] ICollection<StockQuoteDto> stockQuote) => stockQuote;
    
    [Subscribe]
    [Topic("PortfolioDeleted")]
    public Guid OnPortfolioDeleted([EventMessage] Guid portfolioId) => portfolioId;
    
    
    [Subscribe]
    [Topic("PortfolioUpdated")]
    public Guid OnPortfolioUpdated([EventMessage] Guid portfolioId) => portfolioId;
}