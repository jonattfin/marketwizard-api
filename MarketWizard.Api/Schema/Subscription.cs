using MarketWizard.Application.Features.AddPortfolio.Dto;
using MarketWizard.Domain.Entities;

namespace MarketWizardApi.Schema;

public class Subscription
{
    [Subscribe]
    public PortfolioInput OnPortfolioAdded([EventMessage] PortfolioInput portfolioInput) => portfolioInput;
}