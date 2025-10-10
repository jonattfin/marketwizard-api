namespace MarketWizard.Application.Exceptions;

public class PortfolioNotFoundException(Guid id) : Exception($"Portfolio with ID {id} not found.");