using MediatR;

namespace MarketWizard.Application.Features.Stocks.GetStockBySymbol;

public class GetStockBySymbolQuery : IRequest<StockDto?>
{
  public string Symbol { get; set; }
}

public class GetStockBySymbolQueryHandler : IRequestHandler<GetStockBySymbolQuery, StockDto?>
{
  public async Task<StockDto?> Handle(GetStockBySymbolQuery request, CancellationToken cancellationToken)
  {
    var stock = new StockDto()
    {
      Name = "Amazon",
      Symbol = request.Symbol,
      Description =
        "Amazon.com, Inc. engages in the retail sale of consumer products, advertising, and subscriptions service through online and physical stores in North America and internationally.",
      CurrentPrice = 219.57,
      MarketCap = 2.4e9,
    };
    
    return await Task.FromResult(stock);
  }
}