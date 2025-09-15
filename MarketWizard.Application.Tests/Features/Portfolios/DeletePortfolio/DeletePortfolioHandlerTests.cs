using FluentAssertions;
using HotChocolate.Subscriptions;
using MarketWizard.Application.Contracts.Persistence;
using MarketWizard.Application.Features.Portfolios.DeletePortfolio;
using Moq;

namespace MarketWizard.Application.Tests.Features.Portfolios.DeletePortfolio;

public class DeletePortfolioHandlerTests
{
    private readonly DeletePortfolioHandler _sut;
    private readonly Mock<IPortfolioRepository> _mockPortfolioRepository;
    private readonly Mock<ITopicEventSender> _mockTopicEventSender;

    public DeletePortfolioHandlerTests()
    {
        // Arrange
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        _mockPortfolioRepository = new Mock<IPortfolioRepository>();
        _mockTopicEventSender = new Mock<ITopicEventSender>();

        unitOfWorkMock.Setup(x => x.PortfolioRepository).Returns(_mockPortfolioRepository.Object);
        _sut = new DeletePortfolioHandler(unitOfWorkMock.Object, _mockTopicEventSender.Object);
    }

    [Fact]
    public async Task Handle_Should_Delete_Portfolio_And_Send_Event()
    {
        using var cts = new CancellationTokenSource();
        var cancellationToken = cts.Token;

        // Act
        var request = new DeletePortfolioCommand(Guid.NewGuid());
        var act = () => _sut.Handle(request, cancellationToken);

        // Assert
        await act.Should().NotThrowAsync<Exception>();

        _mockPortfolioRepository.Verify(
            x => x.Delete(request.PortfolioId, cancellationToken), Times.Once);

        _mockTopicEventSender.Verify(
            x => x.SendAsync("PortfolioDeleted", request.PortfolioId, cancellationToken), Times.Once);
    }
}