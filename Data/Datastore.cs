using MarketWizardApi.ViewModels;

namespace MarketWizardApi.Data;

public class Datastore : IDatastore
{
    private readonly IEnumerable<Asset> _watchlistAssets;
    private readonly IEnumerable<Portfolio> _portfolios;

    public Datastore()
    {
        _watchlistAssets = CreateAssets();
        _portfolios = CreatePortfolios();
    }

    private static IEnumerable<Asset> CreateAssets()
    {
        var assets = new List<Asset>
        {
            new(Guid.NewGuid(), 14.54, "VIX", -1, "VIX", AssetType.Index),
            new(Guid.NewGuid(), 24204, "DAX", 300, "DAX", AssetType.Index),
            new(Guid.NewGuid(), 6445, "SPX", 72, "SPX", AssetType.Index),

            new(Guid.NewGuid(), 6445, "ASML", 72, "ASML", AssetType.Stock),
            new(Guid.NewGuid(), 6445, "DECK", 72, "DECK", AssetType.Stock),

            new(Guid.NewGuid(), 6445, "GLD", 72, "GLD", AssetType.Commodity),
            new(Guid.NewGuid(), 6445, "SIL", 72, "SIL", AssetType.Commodity)
        };

        return assets;
    }

    private IEnumerable<Portfolio> CreatePortfolios()
    {
        var images = CreateImages().ToList();
        var random = new Random();
        
        foreach (var i in Enumerable.Range(0, 100))
        {
            yield return new Portfolio()
            {
                Id = i.ToString(),
                Name = $"Portfolio {i}",
                Description = """
                              Designed for investment goals with a short-medium term horizon. 
                              Ideal for investors who value principal conservation but are comfortable with a small degree of risk and volatility to seek some growth potential.
                              This portfolio typically comprises 25% in ETFs containing stocks, 73% in ETFs that replicate bonds, and 2% in cash (EUR)
                              """,
                ImageUrl = images[random.Next(0, images.Count)],
                LastUpdated = DateTime.Now,
                TotalAmount = 100000,
                Risk = RiskLevel.Low,
                AverageAnnualReturn = 0.75,
                StandardDeviation = -16.05,
                SharpeRatio = 5.55,
                MaximumDrawdown = 4.85,
            };
        }

        IEnumerable<string> CreateImages()
        {
            yield return "https://images.unsplash.com/photo-1618044733300-9472054094ee";
            yield return "https://images.unsplash.com/photo-1612178991541-b48cc8e92a4d";
            yield return "https://images.unsplash.com/photo-1506450041641-40545dddaf90";
            yield return "https://images.unsplash.com/photo-1548454934-501d30773413";
        }
    }

    private static IEnumerable<PortfolioNews> CreatePortfolioNewsById(Guid id, CancellationToken cancellationToken)
    {
        var news = new List<PortfolioNews>
        {
            new PortfolioNews()
            {
                Id = Guid.NewGuid(),
                Headline = "Applied Materials before Q3 Earnings",
                Provider = "Zacks",
                Symbol = "DECK",
                Time = "one day ago",
                PortfolioId = ""
            }
        };

        return news;
    }

    public IEnumerable<Asset> GetWatchlistAssets(CancellationToken cancellationToken)
        => _watchlistAssets;

    public IEnumerable<Portfolio> GetPortfolios(CancellationToken cancellationToken)
        => _portfolios;    
    
    public Portfolio? GetPortfolioById(string id, CancellationToken cancellationToken)
        => _portfolios.FirstOrDefault(p => p.Id == id);

    public IEnumerable<PortfolioNews> GetPortfolioNewsById(string id, CancellationToken cancellationToken)
        => CreatePortfolioNewsById(Guid.Parse(id), cancellationToken);

    public PortfolioPerformance? GetPortfolioPerformanceById(string id, CancellationToken cancellationToken)
    {
        return new PortfolioPerformance()
        {
            Id = Guid.NewGuid(),
            PortfolioId = id,
            Ratio = new PortfolioRatio()
            {
                Beta = 1,
                Sharpe = 2,
                Sortino = 4
            },
            Returns = new List<string>() { "ASML", "SU", "VWRL", "DECK" }.Select(symbol => new PortfolioReturns()
            {
                AssetId = Guid.NewGuid(),
                AssetName = symbol,
                Months = Enumerable.Range(0, 12).Select((i) => i * new Random().NextDouble()),
                Weeks = Enumerable.Range(0, 52).Select((i) => i * new Random().NextDouble())
            })
        };
    }

    public IEnumerable<PortfolioAsset> GetPortfolioAssetsById(string id, CancellationToken cancellationToken)
    {
        return new List<PortfolioAsset>()
        {
            new PortfolioAsset()
            {
                Symbol = "ASML",
                NumberOfShares = 5,
                PricePerShare = 500
            },
            new PortfolioAsset()
            {
                Symbol = "DECK",
                NumberOfShares = 5,
                PricePerShare = 100
            }
        };
    }
}