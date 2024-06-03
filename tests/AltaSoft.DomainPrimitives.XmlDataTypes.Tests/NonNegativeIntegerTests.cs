namespace AltaSoft.DomainPrimitives.XmlDataTypes.Tests;

/// <summary>
/// This class contains unit tests for the NonNegativeInteger class.
/// </summary>
public class NonNegativeIntegerTests
{
    [Theory]
    [InlineData(1)]
    [InlineData(15)]
    [InlineData(31)]
    public void NonNegativeInteger_WhenValueIsNonNegative_ShouldBeValid(int value)
    {
        // Act & Assert
        Assert.True(NonNegativeInteger.Validate(value).IsValid);
    }

    [Fact]
    public void NonNegativeInteger_WhenValueIsZero_ShouldBeValid()
    {
        // Arrange
        const int value = 0;

        // Act & Assert
        Assert.True(NonNegativeInteger.Validate(value).IsValid);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(-15)]
    [InlineData(-31)]
    public void NonNegativeInteger_WhenValueIsNegative_ShouldBeInvalid(int value)
    {
        // Act & Assert
        Assert.False(NonNegativeInteger.Validate(value).IsValid);
    }
}
