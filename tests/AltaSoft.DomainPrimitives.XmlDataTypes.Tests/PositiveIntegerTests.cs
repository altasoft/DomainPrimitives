namespace AltaSoft.DomainPrimitives.XmlDataTypes.Tests;

/// <summary>
/// This class contains unit tests for the Positive class.
/// </summary>
public class PositiveIntegerTests
{
    [Theory]
    [InlineData(1)]
    [InlineData(15)]
    [InlineData(31)]
    public void PositiveInteger_WhenValueIsPositive_ShouldBeValid(int value)
    {
        // Act & Assert
        Assert.True(PositiveInteger.Validate(value).IsValid);
    }

    [Fact]
    public void PositiveInteger_WhenValueIsZero_ShouldThrowException()
    {
        // Arrange
        const int value = 0;

        // Act & Assert
        Assert.False(PositiveInteger.Validate(value).IsValid);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(-15)]
    [InlineData(-31)]
    public void PositiveInteger_WhenValueIsNegative_ShouldThrowException(int value)
    {
        // Act & Assert
        Assert.False(PositiveInteger.Validate(value).IsValid);
    }
}
