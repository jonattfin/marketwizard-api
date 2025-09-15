using AutoFixture;
using FluentAssertions;
using HotChocolate.Subscriptions;
using MarketWizard.Application.Contracts.Persistence;
using MarketWizard.Application.Features.Portfolios.UpdatePortfolio;
using MarketWizard.Domain.Entities;
using Moq;

namespace MarketWizard.Application.Tests.Features.Portfolios.UpdatePortfolio;

public class UpdatePortfolioHandlerTests
{
    private readonly UpdatePortfolioHandler _sut;
    private readonly Mock<IPortfolioRepository> _mockPortfolioRepository;
    private readonly Mock<ITopicEventSender> _mockTopicEventSender;
    private readonly Fixture _fixture = FixtureFactory.Create();

    public UpdatePortfolioHandlerTests()
    {
        // Arrange
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        _mockPortfolioRepository = new Mock<IPortfolioRepository>();
        _mockTopicEventSender = new Mock<ITopicEventSender>();

        unitOfWorkMock.Setup(x => x.PortfolioRepository).Returns(_mockPortfolioRepository.Object);
        _sut = new UpdatePortfolioHandler(unitOfWorkMock.Object, _mockTopicEventSender.Object);
    }

    [Fact]
    public async Task Handle_Should_Delete_Portfolio_And_Send_Event()
    {
        using var cts = new CancellationTokenSource();
        var cancellationToken = cts.Token;

        var request = _fixture.Create<UpdatePortfolioCommand>();
        var portfolio = _fixture.Build<Portfolio>()
            .With(x => x.Id, request.UpdatePortfolio.Id)
            .Create();

        _mockPortfolioRepository.Setup(x => x.GetById(request.UpdatePortfolio.Id, cancellationToken))
            .ReturnsAsync(portfolio);

        // Act
        var act = () => _sut.Handle(request, cancellationToken);

        // Assert
        await act.Should().NotThrowAsync<Exception>();

        _mockPortfolioRepository.Verify(
            x => x.Update(It.IsAny<Portfolio>()), Times.Once);

        _mockTopicEventSender.Verify(
            x => x.SendAsync("PortfolioUpdated", request.UpdatePortfolio.Id, cancellationToken), Times.Once);
    }
}