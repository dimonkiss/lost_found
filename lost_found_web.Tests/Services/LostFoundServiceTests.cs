using lost_found_web.Models;
using lost_found_web.Services;

namespace lost_found_web.Tests.Services;

public class LostFoundServiceTests
{
    private readonly LostFoundService _service;

    public LostFoundServiceTests()
    {
        _service = new LostFoundService();
    }

    #region Lost Items Tests

    [Fact]
    public async Task GetLostItemsAsync_ShouldReturnAllLostItems()
    {
        // Act
        var result = await _service.GetLostItemsAsync();

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCountGreaterThan(0);
        result.Should().OnlyContain(item => !item.IsFound);
    }

    [Fact]
    public async Task GetLostItemByIdAsync_WithValidId_ShouldReturnItem()
    {
        // Arrange
        var lostItems = await _service.GetLostItemsAsync();
        var firstItem = lostItems.First();

        // Act
        var result = await _service.GetLostItemByIdAsync(firstItem.Id);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(firstItem);
    }

    [Fact]
    public async Task GetLostItemByIdAsync_WithInvalidId_ShouldReturnNull()
    {
        // Act
        var result = await _service.GetLostItemByIdAsync(999);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task AddLostItemAsync_ShouldAddItemWithCorrectProperties()
    {
        // Arrange
        var newItem = new LostItem
        {
            ItemName = "Test Item",
            Description = "Test Description",
            LostLocation = "Test Location",
            LostDate = DateTime.Now,
            ContactInfo = "test@example.com"
        };

        // Act
        var result = await _service.AddLostItemAsync(newItem);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().BeGreaterThan(0);
        result.ItemName.Should().Be(newItem.ItemName);
        result.Description.Should().Be(newItem.Description);
        result.LostLocation.Should().Be(newItem.LostLocation);
        result.LostDate.Should().Be(newItem.LostDate);
        result.ContactInfo.Should().Be(newItem.ContactInfo);
        result.CreatedAt.Should().BeCloseTo(DateTime.Now, TimeSpan.FromSeconds(1));
        result.IsFound.Should().BeFalse();
    }

    [Fact]
    public async Task UpdateLostItemAsync_WithValidItem_ShouldUpdateItem()
    {
        // Arrange
        var lostItems = await _service.GetLostItemsAsync();
        var existingItem = lostItems.First();
        existingItem.ItemName = "Updated Item Name";
        existingItem.Description = "Updated Description";

        // Act
        var result = await _service.UpdateLostItemAsync(existingItem);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(existingItem);

        // Verify the item was actually updated
        var updatedItem = await _service.GetLostItemByIdAsync(existingItem.Id);
        updatedItem.Should().NotBeNull();
        updatedItem!.ItemName.Should().Be("Updated Item Name");
        updatedItem.Description.Should().Be("Updated Description");
    }

    [Fact]
    public async Task UpdateLostItemAsync_WithInvalidId_ShouldReturnNull()
    {
        // Arrange
        var nonExistentItem = new LostItem
        {
            Id = 999,
            ItemName = "Non-existent Item"
        };

        // Act
        var result = await _service.UpdateLostItemAsync(nonExistentItem);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task DeleteLostItemAsync_WithValidId_ShouldReturnTrue()
    {
        // Arrange
        var lostItems = await _service.GetLostItemsAsync();
        var itemToDelete = lostItems.First();

        // Act
        var result = await _service.DeleteLostItemAsync(itemToDelete.Id);

        // Assert
        result.Should().BeTrue();

        // Verify the item was actually deleted
        var deletedItem = await _service.GetLostItemByIdAsync(itemToDelete.Id);
        deletedItem.Should().BeNull();
    }

    [Fact]
    public async Task DeleteLostItemAsync_WithInvalidId_ShouldReturnFalse()
    {
        // Act
        var result = await _service.DeleteLostItemAsync(999);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public async Task SearchLostItemsAsync_WithValidSearchTerm_ShouldReturnMatchingItems()
    {
        // Arrange
        var searchTerm = "iPhone";

        // Act
        var result = await _service.SearchLostItemsAsync(searchTerm);

        // Assert
        result.Should().NotBeNull();
        result.Should().OnlyContain(item => 
            item.ItemName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
            item.Description.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
            item.LostLocation.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
    }

    [Fact]
    public async Task SearchLostItemsAsync_WithEmptySearchTerm_ShouldReturnAllItems()
    {
        // Act
        var result = await _service.SearchLostItemsAsync("");

        // Assert
        var allItems = await _service.GetLostItemsAsync();
        result.Should().BeEquivalentTo(allItems);
    }

    [Fact]
    public async Task SearchLostItemsAsync_WithNullSearchTerm_ShouldReturnAllItems()
    {
        // Act
        var result = await _service.SearchLostItemsAsync(null!);

        // Assert
        var allItems = await _service.GetLostItemsAsync();
        result.Should().BeEquivalentTo(allItems);
    }

    #endregion

    #region Found Items Tests

    [Fact]
    public async Task GetFoundItemsAsync_ShouldReturnAllFoundItems()
    {
        // Act
        var result = await _service.GetFoundItemsAsync();

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCountGreaterThan(0);
        result.Should().OnlyContain(item => !item.IsClaimed);
    }

    [Fact]
    public async Task GetFoundItemByIdAsync_WithValidId_ShouldReturnItem()
    {
        // Arrange
        var foundItems = await _service.GetFoundItemsAsync();
        var firstItem = foundItems.First();

        // Act
        var result = await _service.GetFoundItemByIdAsync(firstItem.Id);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(firstItem);
    }

    [Fact]
    public async Task GetFoundItemByIdAsync_WithInvalidId_ShouldReturnNull()
    {
        // Act
        var result = await _service.GetFoundItemByIdAsync(999);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task AddFoundItemAsync_ShouldAddItemWithCorrectProperties()
    {
        // Arrange
        var newItem = new FoundItem
        {
            ItemName = "Test Found Item",
            Description = "Test Found Description",
            FoundLocation = "Test Found Location",
            FoundDate = DateTime.Now,
            ContactInfo = "found@example.com"
        };

        // Act
        var result = await _service.AddFoundItemAsync(newItem);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().BeGreaterThan(0);
        result.ItemName.Should().Be(newItem.ItemName);
        result.Description.Should().Be(newItem.Description);
        result.FoundLocation.Should().Be(newItem.FoundLocation);
        result.FoundDate.Should().Be(newItem.FoundDate);
        result.ContactInfo.Should().Be(newItem.ContactInfo);
        result.CreatedAt.Should().BeCloseTo(DateTime.Now, TimeSpan.FromSeconds(1));
        result.IsClaimed.Should().BeFalse();
    }

    [Fact]
    public async Task UpdateFoundItemAsync_WithValidItem_ShouldUpdateItem()
    {
        // Arrange
        var foundItems = await _service.GetFoundItemsAsync();
        var existingItem = foundItems.First();
        existingItem.ItemName = "Updated Found Item Name";
        existingItem.Description = "Updated Found Description";

        // Act
        var result = await _service.UpdateFoundItemAsync(existingItem);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(existingItem);

        // Verify the item was actually updated
        var updatedItem = await _service.GetFoundItemByIdAsync(existingItem.Id);
        updatedItem.Should().NotBeNull();
        updatedItem!.ItemName.Should().Be("Updated Found Item Name");
        updatedItem.Description.Should().Be("Updated Found Description");
    }

    [Fact]
    public async Task UpdateFoundItemAsync_WithInvalidId_ShouldReturnNull()
    {
        // Arrange
        var nonExistentItem = new FoundItem
        {
            Id = 999,
            ItemName = "Non-existent Found Item"
        };

        // Act
        var result = await _service.UpdateFoundItemAsync(nonExistentItem);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task DeleteFoundItemAsync_WithValidId_ShouldReturnTrue()
    {
        // Arrange
        var foundItems = await _service.GetFoundItemsAsync();
        var itemToDelete = foundItems.First();

        // Act
        var result = await _service.DeleteFoundItemAsync(itemToDelete.Id);

        // Assert
        result.Should().BeTrue();

        // Verify the item was actually deleted
        var deletedItem = await _service.GetFoundItemByIdAsync(itemToDelete.Id);
        deletedItem.Should().BeNull();
    }

    [Fact]
    public async Task DeleteFoundItemAsync_WithInvalidId_ShouldReturnFalse()
    {
        // Act
        var result = await _service.DeleteFoundItemAsync(999);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public async Task SearchFoundItemsAsync_WithValidSearchTerm_ShouldReturnMatchingItems()
    {
        // Arrange
        var searchTerm = "Student";

        // Act
        var result = await _service.SearchFoundItemsAsync(searchTerm);

        // Assert
        result.Should().NotBeNull();
        result.Should().OnlyContain(item => 
            item.ItemName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
            item.Description.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
            item.FoundLocation.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
    }

    [Fact]
    public async Task SearchFoundItemsAsync_WithEmptySearchTerm_ShouldReturnAllItems()
    {
        // Act
        var result = await _service.SearchFoundItemsAsync("");

        // Assert
        var allItems = await _service.GetFoundItemsAsync();
        result.Should().BeEquivalentTo(allItems);
    }

    [Fact]
    public async Task SearchFoundItemsAsync_WithNullSearchTerm_ShouldReturnAllItems()
    {
        // Act
        var result = await _service.SearchFoundItemsAsync(null!);

        // Assert
        var allItems = await _service.GetFoundItemsAsync();
        result.Should().BeEquivalentTo(allItems);
    }

    #endregion
}
