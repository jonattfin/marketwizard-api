using MarketWizard.Domain;
using MarketWizard.Domain.Entities;

namespace MarketWizardApi.Schema;

public class Subscription
{
    [Subscribe]
    public Portfolio OnPortfolioAdded([EventMessage] Portfolio portfolio) => portfolio;
}