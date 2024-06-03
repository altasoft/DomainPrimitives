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
        Assert.True(NegativeInteger.Validate(value).IsValid);
    }

    [Fact]
    public void NegativeInteger_WhenValueIsZero_ShouldBeInvalid()
    {
        // Arrange
        const int value = 0;

        // Act & Assert
        Assert.False(NegativeInteger.Validate(value).IsValid);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(15)]
    [InlineData(31)]
    public void NegativeInteger_WhenValueIsNegative_ShouldBeInvalid(int value)
    {
        // Act & Assert
        Assert.False(NegativeInteger.Validate(value).IsValid);
    }
}
