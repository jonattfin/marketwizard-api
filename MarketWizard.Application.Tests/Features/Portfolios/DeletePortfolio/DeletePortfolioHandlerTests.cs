using FluentAssertions;
using HotChocolate.Subscriptions;
using MarketWizard.Application.Contracts.Persistence;
using MarketWizard.Application.Features.Portfolios.DeletePortfolio;
using MediatR;
using Moq;

namespace MarketWizard.Application.Tests.Features.Portfolios.DeletePortfolio;

public class DeletePortfolioHandlerTests
{
    private readonly DeletePortfolioHandler _sut;
    private readonly Mock<IPortfolioRepository> _mockPortfolioRepository;
    private readonly Mock<IMediator> _mockMediator;

    public DeletePortfolioHandlerTests()
    {
        // Arrange
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        _mockPortfolioRepository = new Mock<IPortfolioRepository>();
        _mockMediator = new Mock<IMediator>();

        unitOfWorkMock.Setup(x => x.PortfolioRepository).Returns(_mockPortfolioRepository.Object);
        _sut = new DeletePortfolioHandler(unitOfWorkMock.Object, _mockMediator.Object);
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
        
         _mockMediator.Verify(
            x => x.Publish(It.IsAny<DeletePortfolioNotification>(), cancellationToken), Times.Once);
    }
}