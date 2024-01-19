namespace AltaSoft.DomainPrimitives.Tests;

/// <summary>
/// This class contains unit tests for the GMonthDay class.
/// </summary>
public class GMonthDayTests
{
	[Theory]
	[InlineData(1, 1)]
	[InlineData(12, 31)]
	[InlineData(2, 28)]
	[InlineData(3, 15)]
	public void Validate_ShouldNotThrowException_WhenValidDateOnly(int month, int day)
	{
		// Arrange
		var validDateOnly = new DateOnly(2000, month, day);

		// Act & Assert
		var exception = Record.Exception(() => GMonthDay.Validate(validDateOnly));
		Assert.Null(exception);
	}

	[Fact]
	public void Default_ShouldReturnDefaultDateOnly()
	{
		// Act
		var defaultDateOnly = GMonthDay.Default;

		// Assert
		Assert.Equal(default, defaultDateOnly);
	}

	[Theory]
	[InlineData(1, 1, "01-01")]
	[InlineData(12, 31, "12-31")]
	[InlineData(2, 28, "02-28")]
	[InlineData(3, 15, "03-15")]
	public void ToString_ShouldReturnFormattedString(int month, int day, string expectedString)
	{
		// Arrange
		var dateOnly = new DateOnly(2000, month, day);

		// Act
		var result = GMonthDay.ToString(dateOnly);

		// Assert
		Assert.Equal(expectedString, result);
	}
}