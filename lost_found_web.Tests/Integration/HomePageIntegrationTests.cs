using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;

namespace lost_found_web.Tests.Integration;

public class HomePageIntegrationTests : IClassFixture<WebApplicationFactory>
{
    private readonly WebApplicationFactory _factory;
    private readonly HttpClient _client;

    public HomePageIntegrationTests(WebApplicationFactory factory)
    {
        _factory = factory;
        _client = _factory.CreateClient();
    }

    [Fact]
    public async Task HomePage_ShouldReturnSuccessStatusCode()
    {
        // Act
        var response = await _client.GetAsync("/");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task HomePage_ShouldContainExpectedContent()
    {
        // Act
        var response = await _client.GetAsync("/");
        var content = await response.Content.ReadAsStringAsync();

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        content.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task PrivacyPage_ShouldReturnSuccessStatusCode()
    {
        // Act
        var response = await _client.GetAsync("/Privacy");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task ErrorPage_ShouldReturnSuccessStatusCode()
    {
        // Act
        var response = await _client.GetAsync("/Error");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}
