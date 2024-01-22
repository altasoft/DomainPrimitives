namespace AltaSoft.DomainPrimitives.XmlDataTypes.Tests;

/// <summary>
/// Contains unit tests for the GYearMonth class.
/// </summary>
public class GYearMonthTests
{
	[Theory]
	[InlineData(2022, 1)]
	[InlineData(2024, 12)]
	[InlineData(2024, 2)]
	[InlineData(2023, 3)]
	public void Validate_ShouldNotThrowException_WhenValidDateOnly(int year, int month)
	{
		// Arrange
		var validDateOnly = new DateOnly(year, month, 1);

		// Act & Assert
		var exception = Record.Exception(() => GYearMonth.Validate(validDateOnly));
		Assert.Null(exception);
	}

	[Fact]
	public void Default_ShouldReturnDefaultDateOnly()
	{
		// Act
		var defaultDateOnly = GYearMonth.Default;

		// Assert
		Assert.Equal(default, defaultDateOnly);
	}

	[Theory]
	[InlineData(2022, 1, "2022-01")]
	[InlineData(2024, 12, "2024-12")]
	[InlineData(2024, 2, "2024-02")]
	[InlineData(2023, 3, "2023-03")]
	public void ToString_ShouldReturnFormattedString(int year, int month, string expectedString)
	{
		// Arrange
		var dateOnly = new DateOnly(year, month, 1);

		// Act
		var result = GYearMonth.ToString(dateOnly);

		// Assert
		Assert.Equal(expectedString, result);
	}
}