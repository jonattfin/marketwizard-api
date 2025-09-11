using MarketWizard.Application.Dto;

namespace MarketWizard.Application.Interfaces.Infra;

public interface IFinnhubService
{
    Task<StockQuote?> GetStockQuote(string symbol);
    
    Task<List<StockQuote>> GetMultipleStockQuote(List<string> symbols);
}