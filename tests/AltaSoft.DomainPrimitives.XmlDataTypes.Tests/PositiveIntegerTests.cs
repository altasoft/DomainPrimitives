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
    public void PositiveInteger_WhenValueIsPositive_ShouldNotThrowException(int value)
    {
        // Act & Assert
        var exception = Record.Exception(() => PositiveInteger.Validate(value));
        Assert.Null(exception);
    }

    [Fact]
    public void PositiveInteger_WhenValueIsZero_ShouldThrowException()
    {
        // Arrange
        const int value = 0;

        // Act & Assert
        Assert.Throws<InvalidDomainValueException>(() => PositiveInteger.Validate(value));
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(-15)]
    [InlineData(-31)]
    public void PositiveInteger_WhenValueIsNegative_ShouldThrowException(int value)
    {
        // Act & Assert
        Assert.Throws<InvalidDomainValueException>(() => PositiveInteger.Validate(value));
    }

    [Fact]
    public void PositiveInteger_DefaultValue_ShouldBeOne()
    {
        // Arrange
        const int expectedValue = 1;

        // Act & Assert
        Assert.Equal(expectedValue, PositiveInteger.Default);
    }
}