using FluentAssertions;
using WordFinder.Service.Services;

public class WordFinderServiceTests
{
    private readonly WordFinderService _service;

    public WordFinderServiceTests()
    {
        _service = new WordFinderService();
    }

    [Fact]
    public void SetMatrix_Should_Throw_When_Null()
    {
        Action act = () => _service.SetMatrix(null!);
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void SetMatrix_Should_Throw_When_InconsistentRows()
    {
        var matrix = new List<string> { "abc", "abcd" };
        Action act = () => _service.SetMatrix(matrix);
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Find_Should_Return_CorrectWords()
    {
        var matrix = new List<string> { "xchill", "xcoldx", "ywindy", "zwarmz", "kkhotk", "qqdryq" };
        var wordstream = new List<string> { "chill", "cold", "wind", "warm", "hot", "dry", "snow" };

        _service.SetMatrix(matrix);
        var result = _service.Find(wordstream);

        result.Should().Contain(new[] { "chill", "cold", "wind", "warm", "hot", "dry" });
        result.Should().NotContain("snow");
    }
}
