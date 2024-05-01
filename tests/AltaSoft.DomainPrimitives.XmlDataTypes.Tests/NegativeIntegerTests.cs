namespace AltaSoft.DomainPrimitives.XmlDataTypes.Tests;

/// <summary>
/// This class contains unit tests for the NegativeInteger class.
/// </summary>
public class NegativeIntegerTests
{
    [Theory]
    [InlineData(-1)]
    [InlineData(-15)]
    [InlineData(-31)]
    public void NegativeInteger_WhenValueIsNegative_ShouldNotThrowException(int value)
    {
        // Act & Assert
        var exception = Record.Exception(() => NegativeInteger.Validate(value));
        Assert.Null(exception);
    }

    [Fact]
    public void NegativeInteger_WhenValueIsZero_ShouldThrowException()
    {
        // Arrange
        const int value = 0;

        // Act & Assert
        Assert.Throws<InvalidDomainValueException>(() => NegativeInteger.Validate(value));
    }

    [Theory]
    [InlineData(1)]
    [InlineData(15)]
    [InlineData(31)]
    public void NegativeInteger_WhenValueIsNegative_ShouldThrowException(int value)
    {
        // Act & Assert
        Assert.Throws<InvalidDomainValueException>(() => NegativeInteger.Validate(value));
    }
}
