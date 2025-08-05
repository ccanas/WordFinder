using FluentValidation;
using WordFinder.Features;
using FluentAssertions;

namespace WordFinder.Tests.UnitTest
{
    public class GetFoundWordsRequestTests
    {
        [Fact]
        public void Constructor_ValidParameters_ShouldCreateInstance()
        {
            // Arrange
            var matrix = new List<string> { "ABC", "DEF", "GHI" };
            var wordStream = new List<string> { "ABC", "GHI" };

            // Act
            var request = new GetFoundWordsRequest(matrix, wordStream);

            // Assert
            request.Matrix.Should().BeEquivalentTo(matrix);
            request.WordStream.Should().BeEquivalentTo(wordStream);
        }

        [Fact]
        public void Constructor_EmptyMatrix_ShouldThrowValidationException()
        {
            // Arrange
            var matrix = new List<string>();
            var wordStream = new List<string> { "ABC" };

            // Act
            Action act = () => new GetFoundWordsRequest(matrix, wordStream);

            // Assert
            act.Should().Throw<ValidationException>()
                .WithMessage("*Matrix must contain at least one row*");
        }

        [Fact]
        public void Constructor_MatrixWithDifferentRowLengths_ShouldThrowValidationException()
        {
            // Arrange
            var matrix = new List<string> { "ABC", "DE", "FGHI" };
            var wordStream = new List<string> { "ABC" };

            // Act
            Action act = () => new GetFoundWordsRequest(matrix, wordStream);

            // Assert
            act.Should().Throw<ValidationException>()
                .WithMessage("*All matrix rows must have the same length*");
        }

        [Fact]
        public void Constructor_MatrixWithEmptyRow_ShouldThrowValidationException()
        {
            // Arrange
            var matrix = new List<string> { "ABC", "", "GHI" };
            var wordStream = new List<string> { "ABC" };

            // Act
            Action act = () => new GetFoundWordsRequest(matrix, wordStream);

            // Assert
            act.Should().Throw<ValidationException>()
                .WithMessage("*Matrix rows must not be empty or whitespace*");
        }

        [Fact]
        public void Constructor_EmptyWordStream_ShouldThrowValidationException()
        {
            // Arrange
            var matrix = new List<string> { "ABC", "DEF" };
            var wordStream = new List<string>();

            // Act
            Action act = () => new GetFoundWordsRequest(matrix, wordStream);

            // Assert
            act.Should().Throw<ValidationException>()
                .WithMessage("*WordStream must contain at least one word*");
        }

        [Fact]
        public void Constructor_WordStreamWithEmptyWord_ShouldThrowValidationException()
        {
            // Arrange
            var matrix = new List<string> { "ABC", "DEF" };
            var wordStream = new List<string> { "ABC", "" };

            // Act
            Action act = () => new GetFoundWordsRequest(matrix, wordStream);

            // Assert
            act.Should().Throw<ValidationException>()
                .WithMessage("*WordStream must not contain empty or whitespace words*");
        }
    }
}