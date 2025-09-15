using FluentAssertions;
using HotChocolate.Subscriptions;
using MarketWizard.Application.Contracts.Persistence;
using MarketWizard.Application.Features.Portfolios.DeletePortfolio;
using Moq;

namespace MarketWizard.Application.Tests.Features.Portfolios.DeletePortfolio;

public class DeletePortfolioCommandTests
{
     [Fact]
    public async Task Handle_Should_Delete_Portfolio_And_Send_Event()
    {
        // Arrange
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var repositoryMock = new Mock<IPortfolioRepository>();
        var topicEventSenderMock = new Mock<ITopicEventSender>();

        unitOfWorkMock.Setup(x => x.PortfolioRepository).Returns(repositoryMock.Object);
        var sut = new DeletePortfolioHandler(unitOfWorkMock.Object, topicEventSenderMock.Object);

        using var cts = new CancellationTokenSource();
        var cancellationToken = cts.Token;
        
        // Act
        var request = new DeletePortfolioCommand(Guid.NewGuid());
        var act = () => sut.Handle(request, cancellationToken);
        
        // Assert
        await act.Should().NotThrowAsync<Exception>();
        
        repositoryMock.Verify(
            x => x.Delete(request.PortfolioId, cancellationToken), Times.Once);;;

        topicEventSenderMock.Verify(
            x => x.SendAsync("PortfolioDeleted", request.PortfolioId, cancellationToken), Times.Once);
    }
}