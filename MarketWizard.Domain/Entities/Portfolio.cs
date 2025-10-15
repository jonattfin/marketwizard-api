using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MarketWizard.Domain.Entities.Common;

namespace MarketWizard.Domain.Entities;

public partial class Portfolio : BaseEntity, IUserScopedEntity
{
    public User User { get; set; }
    
    public Guid UserId { get; set; }
    
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    public string ImageUrl { get; set; }
    
    public ICollection<PortfolioAsset> PortfolioAssets { get; set; } = new List<PortfolioAsset>();
    
    [ConcurrencyCheck]
    public Guid Version { get; set; } = Guid.NewGuid();
}

public partial class Portfolio
{
    [NotMapped]
    public double TotalValue => PortfolioAssets.Sum(x => x.NumberOfShares * x.PricePerShare);

    [NotMapped]
    public double UnrealizedGain
    {
        get
        {
            var unrealizedGain = 0.0;
            
            foreach (var portfolioAsset in PortfolioAssets)
            {
                if (portfolioAsset?.Asset?.LastPrice is null) 
                    continue;
                
                unrealizedGain += (portfolioAsset.Asset.LastPrice.Value - portfolioAsset.PricePerShare) * portfolioAsset.NumberOfShares;
            }
            
            return unrealizedGain;
        }
    }
}