namespace AltaSoft.DomainPrimitives.XmlDataTypes.Tests;

/// <summary>
/// This class contains unit tests for the NonPositiveInteger class.
/// </summary>
public class NonPositiveIntegerTests
{
    [Theory]
    [InlineData(-1)]
    [InlineData(-15)]
    [InlineData(-31)]
    public void NonPositiveInteger_WhenValueIsNonPositive_ShouldBeValid(int value)
    {
        // Act & Assert
        Assert.True(NonPositiveInteger.Validate(value).IsValid);
    }

    [Fact]
    public void NonPositiveInteger_WhenValueIsZero_ShouldBeValid()
    {
        // Arrange
        const int value = 0;

        // Act & Assert
        Assert.True(NonPositiveInteger.Validate(value).IsValid);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(15)]
    [InlineData(31)]
    public void NonPositiveInteger_WhenValueIsPositive_ShouldThrowException(int value)
    {
        // Act & Assert
        Assert.False(NonPositiveInteger.Validate(value).IsValid);
    }
}
