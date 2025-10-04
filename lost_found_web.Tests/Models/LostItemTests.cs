using System.ComponentModel.DataAnnotations;
using lost_found_web.Models;

namespace lost_found_web.Tests.Models;

public class LostItemTests
{
    [Fact]
    public void LostItem_WithValidData_ShouldBeValid()
    {
        // Arrange
        var lostItem = new LostItem
        {
            ItemName = "Test Item",
            Description = "Test Description",
            LostLocation = "Test Location",
            LostDate = DateTime.Now,
            ContactInfo = "test@example.com"
        };

        // Act
        var validationResults = ValidateModel(lostItem);

        // Assert
        validationResults.Should().BeEmpty();
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void LostItem_WithEmptyOrNullItemName_ShouldBeInvalid(string itemName)
    {
        // Arrange
        var lostItem = new LostItem
        {
            ItemName = itemName,
            Description = "Test Description",
            LostLocation = "Test Location",
            LostDate = DateTime.Now,
            ContactInfo = "test@example.com"
        };

        // Act
        var validationResults = ValidateModel(lostItem);

        // Assert
        validationResults.Should().Contain(vr => vr.MemberNames.Contains("ItemName"));
    }

    [Fact]
    public void LostItem_WithItemNameTooLong_ShouldBeInvalid()
    {
        // Arrange
        var lostItem = new LostItem
        {
            ItemName = new string('A', 101), // 101 characters
            Description = "Test Description",
            LostLocation = "Test Location",
            LostDate = DateTime.Now,
            ContactInfo = "test@example.com"
        };

        // Act
        var validationResults = ValidateModel(lostItem);

        // Assert
        validationResults.Should().Contain(vr => vr.MemberNames.Contains("ItemName"));
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void LostItem_WithEmptyOrNullDescription_ShouldBeInvalid(string description)
    {
        // Arrange
        var lostItem = new LostItem
        {
            ItemName = "Test Item",
            Description = description,
            LostLocation = "Test Location",
            LostDate = DateTime.Now,
            ContactInfo = "test@example.com"
        };

        // Act
        var validationResults = ValidateModel(lostItem);

        // Assert
        validationResults.Should().Contain(vr => vr.MemberNames.Contains("Description"));
    }

    [Fact]
    public void LostItem_WithDescriptionTooLong_ShouldBeInvalid()
    {
        // Arrange
        var lostItem = new LostItem
        {
            ItemName = "Test Item",
            Description = new string('A', 501), // 501 characters
            LostLocation = "Test Location",
            LostDate = DateTime.Now,
            ContactInfo = "test@example.com"
        };

        // Act
        var validationResults = ValidateModel(lostItem);

        // Assert
        validationResults.Should().Contain(vr => vr.MemberNames.Contains("Description"));
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void LostItem_WithEmptyOrNullLostLocation_ShouldBeInvalid(string lostLocation)
    {
        // Arrange
        var lostItem = new LostItem
        {
            ItemName = "Test Item",
            Description = "Test Description",
            LostLocation = lostLocation,
            LostDate = DateTime.Now,
            ContactInfo = "test@example.com"
        };

        // Act
        var validationResults = ValidateModel(lostItem);

        // Assert
        validationResults.Should().Contain(vr => vr.MemberNames.Contains("LostLocation"));
    }

    [Fact]
    public void LostItem_WithLostLocationTooLong_ShouldBeInvalid()
    {
        // Arrange
        var lostItem = new LostItem
        {
            ItemName = "Test Item",
            Description = "Test Description",
            LostLocation = new string('A', 101), // 101 characters
            LostDate = DateTime.Now,
            ContactInfo = "test@example.com"
        };

        // Act
        var validationResults = ValidateModel(lostItem);

        // Assert
        validationResults.Should().Contain(vr => vr.MemberNames.Contains("LostLocation"));
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void LostItem_WithEmptyOrNullContactInfo_ShouldBeInvalid(string contactInfo)
    {
        // Arrange
        var lostItem = new LostItem
        {
            ItemName = "Test Item",
            Description = "Test Description",
            LostLocation = "Test Location",
            LostDate = DateTime.Now,
            ContactInfo = contactInfo
        };

        // Act
        var validationResults = ValidateModel(lostItem);

        // Assert
        validationResults.Should().Contain(vr => vr.MemberNames.Contains("ContactInfo"));
    }

    [Fact]
    public void LostItem_WithContactInfoTooLong_ShouldBeInvalid()
    {
        // Arrange
        var lostItem = new LostItem
        {
            ItemName = "Test Item",
            Description = "Test Description",
            LostLocation = "Test Location",
            LostDate = DateTime.Now,
            ContactInfo = new string('A', 201) // 201 characters
        };

        // Act
        var validationResults = ValidateModel(lostItem);

        // Assert
        validationResults.Should().Contain(vr => vr.MemberNames.Contains("ContactInfo"));
    }

    [Fact]
    public void LostItem_WithDefaultValues_ShouldHaveCorrectDefaults()
    {
        // Arrange & Act
        var lostItem = new LostItem();

        // Assert
        lostItem.Id.Should().Be(0);
        lostItem.ItemName.Should().Be(string.Empty);
        lostItem.Description.Should().Be(string.Empty);
        lostItem.LostLocation.Should().Be(string.Empty);
        lostItem.LostDate.Should().Be(default(DateTime));
        lostItem.ContactInfo.Should().Be(string.Empty);
        lostItem.ImageUrl.Should().BeNull();
        lostItem.CreatedAt.Should().BeCloseTo(DateTime.Now, TimeSpan.FromSeconds(1));
        lostItem.IsFound.Should().BeFalse();
    }

    private static IList<ValidationResult> ValidateModel(object model)
    {
        var validationResults = new List<ValidationResult>();
        var ctx = new ValidationContext(model, null, null);
        Validator.TryValidateObject(model, ctx, validationResults, true);
        return validationResults;
    }
}
