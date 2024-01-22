namespace AltaSoft.DomainPrimitives.XmlDataTypes.Tests;

/// <summary>
/// This class contains unit tests for the NonPositiveInteger class.
/// </summary>
public class NonPositiveIntegerTests
{
	[Theory]
	[InlineData(-1)]
	[InlineData(-15)]
	[InlineData(-31)]
	public void NonPositiveInteger_WhenValueIsNonPositive_ShouldNotThrowException(int value)
	{
		// Act & Assert
		var exception = Record.Exception(() => NonPositiveInteger.Validate(value));
		Assert.Null(exception);
	}

	[Fact]
	public void NonPositiveInteger_WhenValueIsZero_ShouldNotThrowException()
	{
		// Arrange
		const int value = 0;

		// Act & Assert
		var exception = Record.Exception(() => NonPositiveInteger.Validate(value));
		Assert.Null(exception);
	}

	[Theory]
	[InlineData(1)]
	[InlineData(15)]
	[InlineData(31)]
	public void NonPositiveInteger_WhenValueIsPositive_ShouldThrowException(int value)
	{
		// Act & Assert
		Assert.Throws<InvalidDomainValueException>(() => NonPositiveInteger.Validate(value));
	}

	[Fact]
	public void NonPositiveInteger_DefaultValue_ShouldBeOne()
	{
		// Arrange
		const int expectedValue = 0;

		// Act & Assert
		Assert.Equal(expectedValue, NonPositiveInteger.Default);
	}
}