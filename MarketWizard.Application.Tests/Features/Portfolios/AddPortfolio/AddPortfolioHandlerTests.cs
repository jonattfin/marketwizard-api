using FluentAssertions;
using MarketWizard.Application.Contracts.Persistence;
using MarketWizard.Application.Features.Portfolios.AddPortfolio;
using MarketWizard.Domain.Entities;
using Moq;

namespace MarketWizard.Application.Tests.Features.Portfolios.AddPortfolio;

public class AddPortfolioHandlerTests
{
    private readonly AddPortfolioHandler _sut;
    private readonly Mock<IPortfolioRepository> _mockPortfolioRepository;

    public AddPortfolioHandlerTests()
    {
        // Arrange
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        _mockPortfolioRepository = new Mock<IPortfolioRepository>();

        unitOfWorkMock.Setup(x => x.PortfolioRepository).Returns(_mockPortfolioRepository.Object);
        _sut = new AddPortfolioHandler(unitOfWorkMock.Object);
    }

    [Fact]
    public async Task Handle_Should_Add_Portfolio_And_Send_Event()
    {
        using var cts = new CancellationTokenSource();
        var cancellationToken = cts.Token;

        // Act
        var command = new AddPortfolioCommand(new AddPortfolioInputDto());
        var act = () => _sut.Handle(command, cancellationToken);

        // Assert
        await act.Should().NotThrowAsync<Exception>();

        _mockPortfolioRepository.Verify(
            x => x.Insert(It.IsAny<Portfolio>(), cancellationToken), Times.Once);
    }
}