using MarketWizard.Application.Dto;

namespace MarketWizardApi.Schema;

public class Subscription
{
    [Subscribe]
    [Topic("StocksPriceUpdated")]
    public List<StockQuote> OnStockPriceUpdated([EventMessage] List<StockQuote> stockQuote) => stockQuote;
}