using lost_found_web.Models;

namespace lost_found_web.Tests.Utilities;

public static class TestDataBuilder
{
    public static LostItem CreateValidLostItem(string? itemName = null, string? description = null)
    {
        return new LostItem
        {
            ItemName = itemName ?? "Test Lost Item",
            Description = description ?? "Test Lost Description",
            LostLocation = "Test Lost Location",
            LostDate = DateTime.Now.AddDays(-1),
            ContactInfo = "test@example.com",
            ImageUrl = "https://example.com/image.jpg"
        };
    }

    public static FoundItem CreateValidFoundItem(string? itemName = null, string? description = null)
    {
        return new FoundItem
        {
            ItemName = itemName ?? "Test Found Item",
            Description = description ?? "Test Found Description",
            FoundLocation = "Test Found Location",
            FoundDate = DateTime.Now.AddDays(-1),
            ContactInfo = "found@example.com",
            ImageUrl = "https://example.com/found-image.jpg"
        };
    }

    public static LostItem CreateInvalidLostItem()
    {
        return new LostItem
        {
            ItemName = "", // Invalid: empty name
            Description = "", // Invalid: empty description
            LostLocation = "", // Invalid: empty location
            LostDate = DateTime.Now,
            ContactInfo = "" // Invalid: empty contact info
        };
    }

    public static FoundItem CreateInvalidFoundItem()
    {
        return new FoundItem
        {
            ItemName = "", // Invalid: empty name
            Description = "", // Invalid: empty description
            FoundLocation = "", // Invalid: empty location
            FoundDate = DateTime.Now,
            ContactInfo = "" // Invalid: empty contact info
        };
    }

    public static LostItem CreateLostItemWithLongFields()
    {
        return new LostItem
        {
            ItemName = new string('A', 101), // Too long
            Description = new string('B', 501), // Too long
            LostLocation = new string('C', 101), // Too long
            LostDate = DateTime.Now,
            ContactInfo = new string('D', 201) // Too long
        };
    }

    public static FoundItem CreateFoundItemWithLongFields()
    {
        return new FoundItem
        {
            ItemName = new string('A', 101), // Too long
            Description = new string('B', 501), // Too long
            FoundLocation = new string('C', 101), // Too long
            FoundDate = DateTime.Now,
            ContactInfo = new string('D', 201) // Too long
        };
    }
}
