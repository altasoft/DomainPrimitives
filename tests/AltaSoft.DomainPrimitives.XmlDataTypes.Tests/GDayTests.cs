namespace AltaSoft.DomainPrimitives.XmlDataTypes.Tests;

/// <summary>
/// Contains unit tests for the GDay class.
/// </summary>
public class GDayTests
{
    [Theory]
    [InlineData(1)]
    [InlineData(15)]
    [InlineData(31)]
    public void Validate_ShouldNotThrowException_WhenValidDateOnly(int day)
    {
        // Arrange
        var validDateOnly = new DateOnly(2000, 1, day);

        // Act & Assert
        var exception = Record.Exception(() => GDay.Validate(validDateOnly));
        Assert.Null(exception);
    }

    [Theory]
    [InlineData(1, "01")]
    [InlineData(15, "15")]
    [InlineData(31, "31")]
    public void ToString_ShouldReturnFormattedString(int day, string expectedString)
    {
        // Arrange
        var dateOnly = new DateOnly(2000, 1, day);

        // Act
        var result = GDay.ToString(dateOnly);

        // Assert
        Assert.Equal(expectedString, result);
    }
}
