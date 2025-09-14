using MarketWizard.Application.Dto;

namespace MarketWizard.Application.Contracts.Infra;

public interface IFinnhubService
{
    Task<List<StockQuoteDto>> GetMultipleStockQuote(List<string> symbols);
}