using FluentAssertions;
using HotChocolate.Subscriptions;
using MarketWizard.Application.Features.AddPortfolio;
using MarketWizard.Data.Repositories;
using MarketWizard.Domain.Entities;
using Moq;

namespace MarketWizard.Application.Tests;

public class AddPortfolioCommandTests
{
    [Fact]
    public async Task Handle_Should_Add_Portfolio_And_Send_Event()
    {
        // Arrange
        var repositoryMock = new Mock<IRepository>();
        var topicEventSenderMock = new Mock<ITopicEventSender>();

        repositoryMock.Setup(x => x.AddPortfolio(It.IsAny<Portfolio>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Guid.NewGuid());

        var sut = new AddPortfolioHandler(repositoryMock.Object, topicEventSenderMock.Object);

        // Act
        var portfolioId = await sut.Handle(new AddPortfolioCommand(new Portfolio()), CancellationToken.None);

        // Assert
        portfolioId.Should().NotBeEmpty();

        topicEventSenderMock.Verify(
            x => x.SendAsync("PortfolioAdded", It.IsAny<Portfolio>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}