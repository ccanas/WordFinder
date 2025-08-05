using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WordFinder.Controllers;
using WordFinder.Dtos;
using WordFinder.Features;

namespace WordFinder.Tests.Controllers
{
    public class WordFinderControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly WordFinderController _controller;

        public WordFinderControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new WordFinderController(_mediatorMock.Object);
        }

        [Fact]
        public async Task GetFoundWords_ReturnsOk_WhenRequestIsValid()
        {
            // Arrange
            var dto = new GetFoundWordsDto
            {
                Matrix = new List<string> { "ABC", "DEF" },
                Wordstream = new List<string> { "ABC" }
            };
            var expectedResult = new List<string> { "ABC" };
            _mediatorMock
                .Setup(m => m.Send(It.IsAny<GetFoundWordsRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResult);

            // Act
            var result = await _controller.GetFoundWords(dto, CancellationToken.None);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(expectedResult, okResult.Value);
        }

        [Fact]
        public async Task GetFoundWords_ReturnsBadRequest_WhenValidationExceptionIsThrown()
        {
            // Arrange
            var dto = new GetFoundWordsDto
            {
                Matrix = new List<string> { "ABC", "DEF" },
                Wordstream = new List<string> { "ABC" }
            };
            var errors = new List<ValidationFailure>
            {
                new ValidationFailure("Matrix", "Matrix must not be null.")
            };
            _mediatorMock
                .Setup(m => m.Send(It.IsAny<GetFoundWordsRequest>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new ValidationException(errors));

            // Act
            var result = await _controller.GetFoundWords(dto, CancellationToken.None);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var errorList = Assert.IsAssignableFrom<IEnumerable<object>>(badRequestResult.Value);
            var error = errorList.First();
            Assert.Contains("Matrix", error.ToString());
            Assert.Contains("Matrix must not be null.", error.ToString());
        }
    }
}