using FluentAssertions;
using HotChocolate.Subscriptions;
using MarketWizard.Application.Features.AddPortfolio;
using MarketWizard.Application.Interfaces.Persistence;
using MarketWizard.Domain.Entities;
using Moq;

namespace MarketWizard.Application.Tests;

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

        // Act
        var act = () => sut.Handle(new AddPortfolioCommand(new Portfolio()), CancellationToken.None);
        
        // Assert
        await act.Should().NotThrowAsync<Exception>();

        topicEventSenderMock.Verify(
            x => x.SendAsync("PortfolioAdded", It.IsAny<Portfolio>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}