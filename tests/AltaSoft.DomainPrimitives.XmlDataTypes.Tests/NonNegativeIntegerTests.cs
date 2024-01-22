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
    public void NonNegativeInteger_WhenValueIsNonNegative_ShouldNotThrowException(int value)
    {
        // Act & Assert
        var exception = Record.Exception(() => NonNegativeInteger.Validate(value));
        Assert.Null(exception);
    }

    [Fact]
    public void NonNegativeInteger_WhenValueIsZero_ShouldNotThrowException()
    {
        // Arrange
        const int value = 0;

        // Act & Assert
        var exception = Record.Exception(() => NonNegativeInteger.Validate(value));
        Assert.Null(exception);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(-15)]
    [InlineData(-31)]
    public void NonNegativeInteger_WhenValueIsNegative_ShouldThrowException(int value)
    {
        // Act & Assert
        Assert.Throws<InvalidDomainValueException>(() => NonNegativeInteger.Validate(value));
    }

    [Fact]
    public void NonNegativeInteger_DefaultValue_ShouldBeOne()
    {
        // Arrange
        const int expectedValue = 0;

        // Act & Assert
        Assert.Equal(expectedValue, NonNegativeInteger.Default);
    }
}