using FluentAssertions;
using Moq;
using WordFinder.Application.Features;
using WordFinder.Features;
using WordFinder.Service.Services;

public class GetWordsFindedHandlerTests
{
    [Fact]
    public async Task Handle_Should_Return_Result_From_Service()
    {
        // Arrange
        var mockService = new Mock<IWordFinderService>();
        var expected = new List<string> { "hot", "cold" };

        mockService.Setup(x => x.Find(It.IsAny<IEnumerable<string>>())).Returns(expected);

        var handler = new GetFoundWordsHandler(mockService.Object);
        var request = new GetFoundWordsRequest(
            new[] { "abc", "def", "ghi" },
            new[] { "hot", "cold" });

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(expected);
        mockService.Verify(x => x.SetMatrix(request.Matrix), Times.Once);
        mockService.Verify(x => x.Find(request.WordStream), Times.Once);
    }
}
