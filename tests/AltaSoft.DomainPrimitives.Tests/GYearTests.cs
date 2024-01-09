namespace AltaSoft.DomainPrimitives.Tests;

/// <summary>
/// Contains unit tests for the GYear class.
/// </summary>
public class GYearTests
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
		var exception = Record.Exception(() => GYear.Validate(validDateOnly));
		Assert.Null(exception);
	}

	[Fact]
	public void Default_ShouldReturnDefaultDateOnly()
	{
		// Act
		var defaultDateOnly = GYear.Default;

		// Assert
		Assert.Equal(default, defaultDateOnly);
	}

	[Theory]
	[InlineData(1968, "1968")]
	[InlineData(2000, "2000")]
	[InlineData(2024, "2024")]
	public void ToString_ShouldReturnFormattedString(int year, string expectedString)
	{
		// Arrange
		var dateOnly = new DateOnly(year, 1, 1);

		// Act
		var result = GYear.ToString(dateOnly);

		// Assert
		Assert.Equal(expectedString, result);
	}
}