using System.ComponentModel.DataAnnotations;
using lost_found_web.Models;

namespace lost_found_web.Tests.Models;

public class FoundItemTests
{
    [Fact]
    public void FoundItem_WithValidData_ShouldBeValid()
    {
        // Arrange
        var foundItem = new FoundItem
        {
            ItemName = "Test Found Item",
            Description = "Test Found Description",
            FoundLocation = "Test Found Location",
            FoundDate = DateTime.Now,
            ContactInfo = "found@example.com"
        };

        // Act
        var validationResults = ValidateModel(foundItem);

        // Assert
        validationResults.Should().BeEmpty();
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void FoundItem_WithEmptyOrNullItemName_ShouldBeInvalid(string itemName)
    {
        // Arrange
        var foundItem = new FoundItem
        {
            ItemName = itemName,
            Description = "Test Found Description",
            FoundLocation = "Test Found Location",
            FoundDate = DateTime.Now,
            ContactInfo = "found@example.com"
        };

        // Act
        var validationResults = ValidateModel(foundItem);

        // Assert
        validationResults.Should().Contain(vr => vr.MemberNames.Contains("ItemName"));
    }

    [Fact]
    public void FoundItem_WithItemNameTooLong_ShouldBeInvalid()
    {
        // Arrange
        var foundItem = new FoundItem
        {
            ItemName = new string('A', 101), // 101 characters
            Description = "Test Found Description",
            FoundLocation = "Test Found Location",
            FoundDate = DateTime.Now,
            ContactInfo = "found@example.com"
        };

        // Act
        var validationResults = ValidateModel(foundItem);

        // Assert
        validationResults.Should().Contain(vr => vr.MemberNames.Contains("ItemName"));
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void FoundItem_WithEmptyOrNullDescription_ShouldBeInvalid(string description)
    {
        // Arrange
        var foundItem = new FoundItem
        {
            ItemName = "Test Found Item",
            Description = description,
            FoundLocation = "Test Found Location",
            FoundDate = DateTime.Now,
            ContactInfo = "found@example.com"
        };

        // Act
        var validationResults = ValidateModel(foundItem);

        // Assert
        validationResults.Should().Contain(vr => vr.MemberNames.Contains("Description"));
    }

    [Fact]
    public void FoundItem_WithDescriptionTooLong_ShouldBeInvalid()
    {
        // Arrange
        var foundItem = new FoundItem
        {
            ItemName = "Test Found Item",
            Description = new string('A', 501), // 501 characters
            FoundLocation = "Test Found Location",
            FoundDate = DateTime.Now,
            ContactInfo = "found@example.com"
        };

        // Act
        var validationResults = ValidateModel(foundItem);

        // Assert
        validationResults.Should().Contain(vr => vr.MemberNames.Contains("Description"));
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void FoundItem_WithEmptyOrNullFoundLocation_ShouldBeInvalid(string foundLocation)
    {
        // Arrange
        var foundItem = new FoundItem
        {
            ItemName = "Test Found Item",
            Description = "Test Found Description",
            FoundLocation = foundLocation,
            FoundDate = DateTime.Now,
            ContactInfo = "found@example.com"
        };

        // Act
        var validationResults = ValidateModel(foundItem);

        // Assert
        validationResults.Should().Contain(vr => vr.MemberNames.Contains("FoundLocation"));
    }

    [Fact]
    public void FoundItem_WithFoundLocationTooLong_ShouldBeInvalid()
    {
        // Arrange
        var foundItem = new FoundItem
        {
            ItemName = "Test Found Item",
            Description = "Test Found Description",
            FoundLocation = new string('A', 101), // 101 characters
            FoundDate = DateTime.Now,
            ContactInfo = "found@example.com"
        };

        // Act
        var validationResults = ValidateModel(foundItem);

        // Assert
        validationResults.Should().Contain(vr => vr.MemberNames.Contains("FoundLocation"));
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void FoundItem_WithEmptyOrNullContactInfo_ShouldBeInvalid(string contactInfo)
    {
        // Arrange
        var foundItem = new FoundItem
        {
            ItemName = "Test Found Item",
            Description = "Test Found Description",
            FoundLocation = "Test Found Location",
            FoundDate = DateTime.Now,
            ContactInfo = contactInfo
        };

        // Act
        var validationResults = ValidateModel(foundItem);

        // Assert
        validationResults.Should().Contain(vr => vr.MemberNames.Contains("ContactInfo"));
    }

    [Fact]
    public void FoundItem_WithContactInfoTooLong_ShouldBeInvalid()
    {
        // Arrange
        var foundItem = new FoundItem
        {
            ItemName = "Test Found Item",
            Description = "Test Found Description",
            FoundLocation = "Test Found Location",
            FoundDate = DateTime.Now,
            ContactInfo = new string('A', 201) // 201 characters
        };

        // Act
        var validationResults = ValidateModel(foundItem);

        // Assert
        validationResults.Should().Contain(vr => vr.MemberNames.Contains("ContactInfo"));
    }

    [Fact]
    public void FoundItem_WithDefaultValues_ShouldHaveCorrectDefaults()
    {
        // Arrange & Act
        var foundItem = new FoundItem();

        // Assert
        foundItem.Id.Should().Be(0);
        foundItem.ItemName.Should().Be(string.Empty);
        foundItem.Description.Should().Be(string.Empty);
        foundItem.FoundLocation.Should().Be(string.Empty);
        foundItem.FoundDate.Should().Be(default(DateTime));
        foundItem.ContactInfo.Should().Be(string.Empty);
        foundItem.ImageUrl.Should().BeNull();
        foundItem.CreatedAt.Should().BeCloseTo(DateTime.Now, TimeSpan.FromSeconds(1));
        foundItem.IsClaimed.Should().BeFalse();
    }

    private static IList<ValidationResult> ValidateModel(object model)
    {
        var validationResults = new List<ValidationResult>();
        var ctx = new ValidationContext(model, null, null);
        Validator.TryValidateObject(model, ctx, validationResults, true);
        return validationResults;
    }
}
