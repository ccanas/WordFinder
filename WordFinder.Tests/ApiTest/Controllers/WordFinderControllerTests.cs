using System.Net;
using System.Net.Http.Json;
using Xunit;
using FluentAssertions;

namespace WordFinder.Api.Tests.Controllers;

public class WordFinderControllerTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public WordFinderControllerTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Post_GetFoundWords_ShouldReturn200AndExpectedWords()
    {
        // Arrange
        var request = new
        {
            Matrix = new[]
            {
                "xchill",
                "xcoldx",
                "ywindy",
                "zwarmz",
                "kkhotk",
                "qqdryq"
            },
            WordStream = new[] { "chill", "cold", "wind", "warm", "hot", "dry" }
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/wordfinder", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var result = await response.Content.ReadFromJsonAsync<IEnumerable<string>>();
        result.Should().Contain(new[] { "chill", "cold", "wind", "warm", "hot", "dry" });
    }

    [Fact]
    public async Task Post_GetFoundWords_WithInvalidMatrix_ShouldReturn400()
    {
        var request = new
        {
            Matrix = new[] { "abc", "de" }, // uneven rows
            WordStream = new[] { "abc" }
        };

        var response = await _client.PostAsJsonAsync("/api/wordfinder", request);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var body = await response.Content.ReadAsStringAsync();
        body.Should().Contain("All matrix rows must have the same length");
    }

    [Fact]
    public async Task Post_GetFoundWords_WithEmptyMatrix_ShouldReturn400()
    {
        var request = new
        {
            Matrix = new string[] { }, // empty matrix
            WordStream = new[] { "abc" }
        };
        var response = await _client.PostAsJsonAsync("/api/wordfinder", request);
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var body = await response.Content.ReadAsStringAsync();
        body.Should().Contain("Matrix must contain at least one row");
    }
}