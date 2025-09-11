using MarketWizard.Application.Dto;

namespace MarketWizard.Application.Contracts.Infra;

public interface IFinnhubService
{
    Task<List<StockQuote>> GetMultipleStockQuote(List<string> symbols);
}