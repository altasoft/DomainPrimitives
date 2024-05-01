namespace AltaSoft.DomainPrimitives.XmlDataTypes.Tests;

/// <summary>
/// Contains unit tests for the GMonth class.
/// </summary>
public class GMonthTests
{
    [Theory]
    [InlineData(1)]
    [InlineData(6)]
    [InlineData(12)]
    public void Validate_ShouldNotThrowException_WhenValidDateOnly(int month)
    {
        // Arrange
        var validDateOnly = new DateOnly(2000, month, 1);

        // Act & Assert
        var exception = Record.Exception(() => GMonth.Validate(validDateOnly));
        Assert.Null(exception);
    }

    [Theory]
    [InlineData(1, "01")]
    [InlineData(6, "06")]
    [InlineData(12, "12")]
    public void ToString_ShouldReturnFormattedString(int month, string expectedString)
    {
        // Arrange
        var dateOnly = new DateOnly(2000, month, 1);

        // Act
        var result = GMonth.ToString(dateOnly);

        // Assert
        Assert.Equal(expectedString, result);
    }
}
