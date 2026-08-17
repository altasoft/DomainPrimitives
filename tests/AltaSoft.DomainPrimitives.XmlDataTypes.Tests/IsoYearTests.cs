using AltaSoft.DomainPrimitives.UnitTests;

namespace AltaSoft.DomainPrimitives.XmlDataTypes.Tests;

/// <summary>
/// Contains unit tests for the IsoYear class.
/// </summary>
public class IsoYearTests
{
    [Theory]
    [InlineData(1968)]
    [InlineData(2000)]
    [InlineData(2024)]
    public void Validate_ShouldNotThrowException_WhenValidDateOnly(int year)
    {
        // Arrange
        var validDateOnly = new DateOnly(year, 1, 1);

        // Act & Assert
        var exception = Record.Exception(() => IsoYear.Validate(validDateOnly));
        Assert.Null(exception);
    }
}
