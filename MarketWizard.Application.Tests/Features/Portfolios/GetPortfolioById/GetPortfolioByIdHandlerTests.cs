using FluentAssertions;

using MarketWizard.Application.Contracts.Persistence;
using MarketWizard.Application.Features.Portfolios.GetPortfolioById;
using Moq;

namespace MarketWizard.Application.Tests.Features.Portfolios.GetPortfolioById;

public class GetPortfolioByIdHandlerTests
{
    private readonly GetPortfolioByIdQueryHandler _sut;
    private readonly Mock<IPortfolioRepository> _mockPortfolioRepository;

    public GetPortfolioByIdHandlerTests()
    {
        // Arrange
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        _mockPortfolioRepository = new Mock<IPortfolioRepository>();

        unitOfWorkMock.Setup(x => x.PortfolioRepository).Returns(_mockPortfolioRepository.Object);
        _sut = new GetPortfolioByIdQueryHandler(unitOfWorkMock.Object);
    }
    
     [Fact]
    public async Task Handle_Should_Get_Portfolio()
    {
        using var cts = new CancellationTokenSource();
        var cancellationToken = cts.Token;

        // Act
        var request = new GetPortfolioByIdQuery(){PortfolioId = Guid.NewGuid()};
        var act = () => _sut.Handle(request, cancellationToken);

        // Assert
        await act.Should().NotThrowAsync<Exception>();

        _mockPortfolioRepository.Verify(
            x => x.GetByIdWithRelatedEntities(request.PortfolioId, cancellationToken), Times.Once);
    }
}