using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;

namespace lost_found_web.Tests.Integration;

public class LostItemsIntegrationTests : IClassFixture<WebApplicationFactory>
{
    private readonly WebApplicationFactory _factory;
    private readonly HttpClient _client;

    public LostItemsIntegrationTests(WebApplicationFactory factory)
    {
        _factory = factory;
        _client = _factory.CreateClient();
    }

    [Fact]
    public async Task LostItems_Index_ShouldReturnSuccessStatusCode()
    {
        // Act
        var response = await _client.GetAsync("/LostItems");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task LostItems_Create_Get_ShouldReturnSuccessStatusCode()
    {
        // Act
        var response = await _client.GetAsync("/LostItems/Create");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task LostItems_Create_Post_WithInvalidData_ShouldReturnBadRequest()
    {
        // Arrange
        var formData = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("LostItem.ItemName", ""), // Invalid: empty name
            new KeyValuePair<string, string>("LostItem.Description", "Test Description"),
            new KeyValuePair<string, string>("LostItem.LostLocation", "Test Location"),
            new KeyValuePair<string, string>("LostItem.LostDate", DateTime.Now.ToString("yyyy-MM-dd")),
            new KeyValuePair<string, string>("LostItem.ContactInfo", "test@example.com")
        });

        // Act
        var response = await _client.PostAsync("/LostItems/Create", formData);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task LostItems_Edit_Get_WithValidId_ShouldReturnSuccessStatusCode()
    {
        // Arrange
        // Use ID 1 which should exist from the sample data in LostFoundService
        var itemId = 1;

        // Act
        var response = await _client.GetAsync($"/LostItems/Edit/{itemId}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task LostItems_Edit_Get_WithInvalidId_ShouldReturnNotFound()
    {
        // Act
        var response = await _client.GetAsync("/LostItems/Edit/999");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task LostItems_Delete_Get_WithValidId_ShouldReturnSuccessStatusCode()
    {
        // Arrange
        // Use ID 1 which should exist from the sample data in LostFoundService
        var itemId = 1;

        // Act
        var response = await _client.GetAsync($"/LostItems/Delete/{itemId}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task LostItems_Delete_Get_WithInvalidId_ShouldReturnNotFound()
    {
        // Act
        var response = await _client.GetAsync("/LostItems/Delete/999");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

}
