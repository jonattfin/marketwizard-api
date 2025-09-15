using FluentAssertions;
using HotChocolate.Subscriptions;
using MarketWizard.Application.Contracts.Persistence;
using MarketWizard.Application.Features.Portfolios.AddPortfolio;
using MarketWizard.Domain.Entities;
using Moq;

namespace MarketWizard.Application.Tests.Features.Portfolios.AddPortfolio;

public class AddPortfolioCommandTests
{
    [Fact]
    public async Task Handle_Should_Add_Portfolio_And_Send_Event()
    {
        // Arrange
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var repositoryMock = new Mock<IPortfolioRepository>();
        var topicEventSenderMock = new Mock<ITopicEventSender>();

        unitOfWorkMock.Setup(x => x.PortfolioRepository).Returns(repositoryMock.Object);
        var sut = new AddPortfolioHandler(unitOfWorkMock.Object, topicEventSenderMock.Object);

        using var cts = new CancellationTokenSource();
        var cancellationToken = cts.Token;
        
        // Act
        var command = new AddPortfolioCommand(new AddPortfolioInputDto());
        var act = () => sut.Handle(command, cancellationToken);
        
        // Assert
        await act.Should().NotThrowAsync<Exception>();
        
        repositoryMock.Verify(
            x => x.Insert(It.IsAny<Portfolio>(), cancellationToken), Times.Once);;;

        topicEventSenderMock.Verify(
            x => x.SendAsync("PortfolioAdded", command.AddPortfolio, cancellationToken), Times.Once);
    }
}