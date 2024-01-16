using AltaSoft.DomainPrimitives.Abstractions;

namespace AltaSoft.DomainPrimitives.Tests;

/// <summary>
/// Test class for Utils methods.
/// </summary>
public class UtilsTests
{
	[Theory]
	[InlineData(2022, 1, 1)]
	[InlineData(2024, 12, 31)]
	[InlineData(2024, 2, 28)]
	[InlineData(2023, 3, 15)]
	public void ToDateTime_DateOnly_ReturnsDateTimeWithMinimumTimeAndLocalKind(int year, int month, int day)
	{
		// Arrange
		var dateOnly = new DateOnly(year, month, day);

		// Act
		var dateTime = dateOnly.ToDateTime();

		// Assert
		Assert.Equal(new DateTime(year, month, day, 0, 0, 0, DateTimeKind.Local), dateTime);
	}

	[Theory]
	[InlineData(12, 30, 0)]
	[InlineData(6, 45, 30)]
	[InlineData(0, 0, 0)]
	[InlineData(23, 59, 59)]
	public void ToDateTime_TimeOnly_ReturnsDateTimeWithMinimumDateAndLocalKind(int hour, int minute, int second)
	{
		// Arrange
		var timeOnly = new TimeOnly(hour, minute, second);

		// Act
		var dateTime = timeOnly.ToDateTime();

		// Assert
		Assert.Equal(new DateTime(DateTime.MinValue.Year, DateTime.MinValue.Month, DateTime.MinValue.Day, hour, minute, second, DateTimeKind.Local), dateTime);
	}
}