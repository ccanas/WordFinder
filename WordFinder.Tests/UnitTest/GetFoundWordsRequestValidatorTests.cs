using FluentValidation.TestHelper;
using WordFinder.Features;

namespace WordFinder.Tests.Features
{
    public class GetFoundWordsRequestValidatorTests
    {
        private readonly GetFoundWordsRequestValidator _validator = new();

        [Fact]
        public void Should_Have_Error_When_Matrix_Is_Null()
        {
            var request = new GetFoundWordsRequest(null, new List<string> { "WORD" });
            var result = _validator.TestValidate(request);
            result.ShouldHaveValidationErrorFor(r => r.Matrix)
                  .WithErrorMessage("Matrix must not be null.");
        }

        [Fact]
        public void Should_Have_Error_When_Matrix_Is_Empty()
        {
            var request = new GetFoundWordsRequest(new List<string>(), new List<string> { "WORD" });
            var result = _validator.TestValidate(request);
            result.ShouldHaveValidationErrorFor(r => r.Matrix)
                  .WithErrorMessage("Matrix must contain at least one row.");
        }

        [Fact]
        public void Should_Have_Error_When_Matrix_Rows_Are_Not_Uniform()
        {
            var request = new GetFoundWordsRequest(new List<string> { "ABC", "DE" }, new List<string> { "WORD" });
            var result = _validator.TestValidate(request);
            result.ShouldHaveValidationErrorFor(r => r.Matrix)
                  .WithErrorMessage("All matrix rows must have the same length.");
        }

        [Fact]
        public void Should_Have_Error_When_Matrix_Row_Is_Whitespace()
        {
            var request = new GetFoundWordsRequest(new List<string> { "ABC", "   " }, new List<string> { "WORD" });
            var result = _validator.TestValidate(request);
            result.ShouldHaveValidationErrorFor(r => r.Matrix)
                  .WithErrorMessage("Matrix rows must not be empty or whitespace.");
        }

        [Fact]
        public void Should_Have_Error_When_WordStream_Is_Null()
        {
            var request = new GetFoundWordsRequest(new List<string> { "ABC" }, null);
            var result = _validator.TestValidate(request);
            result.ShouldHaveValidationErrorFor(r => r.WordStream)
                  .WithErrorMessage("WordStream must not be null.");
        }

        [Fact]
        public void Should_Have_Error_When_WordStream_Is_Empty()
        {
            var request = new GetFoundWordsRequest(new List<string> { "ABC" }, new List<string>());
            var result = _validator.TestValidate(request);
            result.ShouldHaveValidationErrorFor(r => r.WordStream)
                  .WithErrorMessage("WordStream must contain at least one word.");
        }

        [Fact]
        public void Should_Have_Error_When_WordStream_Contains_Whitespace()
        {
            var request = new GetFoundWordsRequest(new List<string> { "ABC" }, new List<string> { "WORD", "   " });
            var result = _validator.TestValidate(request);
            result.ShouldHaveValidationErrorFor(r => r.WordStream)
                  .WithErrorMessage("WordStream must not contain empty or whitespace words.");
        }

        [Fact]
        public void Should_Not_Have_Error_For_Valid_Request()
        {
            var request = new GetFoundWordsRequest(new List<string> { "ABC", "DEF" }, new List<string> { "WORD", "FIND" });
            var result = _validator.TestValidate(request);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}