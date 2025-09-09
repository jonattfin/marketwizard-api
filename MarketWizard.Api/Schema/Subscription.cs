using MarketWizard.Application.Dto;

namespace MarketWizardApi.Schema;

public class Subscription
{
    [Subscribe]
    [Topic("StockPriceUpdated")]
    public StockQuote OnStockPriceUpdated([EventMessage] StockQuote stockQuote) => stockQuote;
}