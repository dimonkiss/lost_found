using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;

namespace lost_found_web.Tests.Integration;

public class SearchIntegrationTests : IClassFixture<WebApplicationFactory>
{
    private readonly WebApplicationFactory _factory;
    private readonly HttpClient _client;

    public SearchIntegrationTests(WebApplicationFactory factory)
    {
        _factory = factory;
        _client = _factory.CreateClient();
    }

    [Fact]
    public async Task Search_Index_ShouldReturnSuccessStatusCode()
    {
        // Act
        var response = await _client.GetAsync("/Search");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Search_WithValidSearchTerm_ShouldReturnSuccessStatusCode()
    {
        // Arrange
        var searchTerm = "iPhone";

        // Act
        var response = await _client.GetAsync($"/Search?searchTerm={searchTerm}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Search_WithEmptySearchTerm_ShouldReturnSuccessStatusCode()
    {
        // Act
        var response = await _client.GetAsync("/Search?searchTerm=");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Search_WithNoSearchTerm_ShouldReturnSuccessStatusCode()
    {
        // Act
        var response = await _client.GetAsync("/Search");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}
