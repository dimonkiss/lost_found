using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;

namespace lost_found_web.Tests.Integration;

public class FoundItemsIntegrationTests : IClassFixture<WebApplicationFactory>
{
    private readonly WebApplicationFactory _factory;
    private readonly HttpClient _client;

    public FoundItemsIntegrationTests(WebApplicationFactory factory)
    {
        _factory = factory;
        _client = _factory.CreateClient();
    }

    [Fact]
    public async Task FoundItems_Index_ShouldReturnSuccessStatusCode()
    {
        // Act
        var response = await _client.GetAsync("/FoundItems");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task FoundItems_Create_Get_ShouldReturnSuccessStatusCode()
    {
        // Act
        var response = await _client.GetAsync("/FoundItems/Create");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task FoundItems_Create_Post_WithInvalidData_ShouldReturnBadRequest()
    {
        // Arrange
        var formData = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("FoundItem.ItemName", ""), // Invalid: empty name
            new KeyValuePair<string, string>("FoundItem.Description", "Test Found Description"),
            new KeyValuePair<string, string>("FoundItem.FoundLocation", "Test Found Location"),
            new KeyValuePair<string, string>("FoundItem.FoundDate", DateTime.Now.ToString("yyyy-MM-dd")),
            new KeyValuePair<string, string>("FoundItem.ContactInfo", "found@example.com")
        });

        // Act
        var response = await _client.PostAsync("/FoundItems/Create", formData);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task FoundItems_Edit_Get_WithValidId_ShouldReturnSuccessStatusCode()
    {
        // Arrange
        // Use ID 1 which should exist from the sample data in LostFoundService
        var itemId = 1;

        // Act
        var response = await _client.GetAsync($"/FoundItems/Edit/{itemId}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task FoundItems_Edit_Get_WithInvalidId_ShouldReturnNotFound()
    {
        // Act
        var response = await _client.GetAsync("/FoundItems/Edit/999");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task FoundItems_Delete_Get_WithValidId_ShouldReturnSuccessStatusCode()
    {
        // Arrange
        // Use ID 1 which should exist from the sample data in LostFoundService
        var itemId = 1;

        // Act
        var response = await _client.GetAsync($"/FoundItems/Delete/{itemId}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task FoundItems_Delete_Get_WithInvalidId_ShouldReturnNotFound()
    {
        // Act
        var response = await _client.GetAsync("/FoundItems/Delete/999");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

}
