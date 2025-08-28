using MarketWizard.Domain;

namespace MarketWizardApi.Schema;

public class Subscription
{
    [Subscribe]
    public Portfolio OnPortfolioAdded([EventMessage] Portfolio portfolio) => portfolio;
}