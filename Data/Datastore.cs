using System.Text;
using MarketWizardApi.ViewModels;

namespace MarketWizardApi.Data;

public class Datastore : IDatastore
{
    private readonly IEnumerable<Asset> _watchlistAssets = CreateAssets();
    private readonly IEnumerable<Portfolio> _portfolios = CreatePortfolios();

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

    private static IEnumerable<Portfolio> CreatePortfolios()
    {
        var portfolios = new List<Portfolio>
        {
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Defensive",
                Description = """
                                Designed for investment goals with a short-term horizon. 
                              It is best suited for investors who prioritize stability and capital preservation over high growth. 
                              This portfolio typically includes 98% in ETFs that replicate bonds and 2% in cash (EUR) to ensure capital preservation.
                              """,
                ImageUrl = "https://images.unsplash.com/photo-1602536422477-f56ba7493ca6",
                LastUpdated = DateTime.Now,
                TotalAmount = 100000,
                Risk = RiskLevel.Low,
                AverageAnnualReturn = 0.02,
                StandardDeviation = -10.28,
                SharpeRatio = 2.09,
                MaximumDrawdown = 6.03,
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Slow and Steady",
                Description = """
                              Designed for investment goals with a short-medium term horizon. 
                              Ideal for investors who value principal conservation but are comfortable with a small degree of risk and volatility to seek some growth potential.
                              This portfolio typically comprises 25% in ETFs containing stocks, 73% in ETFs that replicate bonds, and 2% in cash (EUR)
                              """,
                ImageUrl = "https://images.unsplash.com/photo-1618044733300-9472054094ee",
                LastUpdated = DateTime.Now,
                TotalAmount = 100000,
                Risk = RiskLevel.Low,
                AverageAnnualReturn = 0.75,
                StandardDeviation = -16.05,
                SharpeRatio = 5.55,
                MaximumDrawdown = 4.85,
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Balanced bundle",
                Description = """
                              This portfolio is designed for investment goals with a medium-term horizon. 
                                It is best for investors seeking relatively higher returns while being willing to accept modest risk.
                                This portfolio usually includes a diversified mix of 52% in ETFs containing stocks, 48% in ETFs that replicate bonds, and 2% in cash (EUR).
                              """,
                ImageUrl = "https://images.unsplash.com/photo-1612178991541-b48cc8e92a4d",
                LastUpdated = DateTime.Now,
                TotalAmount = 100000,
                Risk = RiskLevel.Low,
                AverageAnnualReturn = 1.08,
                StandardDeviation = -21.76,
                SharpeRatio = 10.13,
                MaximumDrawdown = 7.58,
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Bold Stack",
                Description = new StringBuilder().Append("""
                                                            Designed for investment goals with a medium-long-term horizon. 
                                                         It is best suited for investors seeking long-term growth with somewhat less variable returns and above-average volatility.
                                                         This portfolio is composed primarily of 70% in ETFs containing stocks, 28% in ETFs that replicate bonds, and 2% in cash (EUR)
                                                         """)
                    .ToString(),
                ImageUrl = "https://images.unsplash.com/photo-1506450041641-40545dddaf90",
                LastUpdated = DateTime.Now,
                TotalAmount = 100000,
                Risk = RiskLevel.Low,
                AverageAnnualReturn = 0.99,
                StandardDeviation = -25.28,
                SharpeRatio = 12.09,
                MaximumDrawdown = 10.6,
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "High growth",
                Description = "",
                ImageUrl = "https://images.unsplash.com/photo-1548454934-501d30773413",
                LastUpdated = DateTime.Now,
                TotalAmount = 100000,
                Risk = RiskLevel.Low,
                AverageAnnualReturn = 1.01,
                StandardDeviation = -29.28,
                SharpeRatio = 15.6,
                MaximumDrawdown = 13.37,
            }
        };

        return portfolios;
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
        => _portfolios.FirstOrDefault(p => p.Id == Guid.Parse(id));

    public IEnumerable<PortfolioNews> GetPortfolioNewsById(string id, CancellationToken cancellationToken)
        => CreatePortfolioNewsById(Guid.Parse(id), cancellationToken);

    public PortfolioPerformance? GetPortfolioPerformanceById(string id, CancellationToken cancellationToken)
    {
        return new PortfolioPerformance()
        {
            Id = Guid.NewGuid(),
            PortfolioId = Guid.Parse(id),
            Ratio = new PortfolioRatio()
            {
                Beta = 0.99,
                Sharpe = 12.09,
                Sortino = 10.6
            },
           Returns = new List<string>() {"ASML", "SU", "VWRL", "DECK"}.Select(symbol => new PortfolioReturns()
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