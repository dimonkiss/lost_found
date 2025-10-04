using lost_found_web.Tests.Utilities;

namespace lost_found_web.Tests;

public class UnitTest1
{
    [Fact]
    public void TestDataBuilder_ShouldCreateValidTestData()
    {
        // Arrange & Act
        var lostItem = TestDataBuilder.CreateValidLostItem();
        var foundItem = TestDataBuilder.CreateValidFoundItem();

        // Assert
        lostItem.Should().NotBeNull();
        lostItem.IsValid().Should().BeTrue();
        
        foundItem.Should().NotBeNull();
        foundItem.IsValid().Should().BeTrue();
    }

    [Fact]
    public void TestExtensions_ShouldValidateModelsCorrectly()
    {
        // Arrange
        var validItem = TestDataBuilder.CreateValidLostItem();
        var invalidItem = TestDataBuilder.CreateInvalidLostItem();

        // Act & Assert
        validItem.IsValid().Should().BeTrue();
        invalidItem.IsValid().Should().BeFalse();
        
        invalidItem.GetValidationErrors().Should().NotBeEmpty();
        invalidItem.GetValidationErrorMembers().Should().NotBeEmpty();
    }
}