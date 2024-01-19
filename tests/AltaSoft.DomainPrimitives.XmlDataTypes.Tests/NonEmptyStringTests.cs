

namespace AltaSoft.DomainPrimitives.Tests;

/// <summary>
/// Contains unit tests for the NonEmptyString class.
/// </summary>
public class NonEmptyStringTests
{
	[Fact]
	public void NonEmptyString_Validate_ValidString_DoesNotThrowException()
	{
		// Arrange
		const string validString = "Hello";

		// Act & Assert
		var exception = Record.Exception(() => AsciiString.Validate(validString));
		Assert.Null(exception);
	}

	[Fact]
	public void NonEmptyString_Validate_NullString_ThrowsException()
	{
		// Arrange
		const string? nullString = null;

		// Act & Assert
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
		Assert.Throws<InvalidDomainValueException>(() => NonEmptyString.Validate(nullString));
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
	}

	[Fact]
	public void NonEmptyString_Validate_EmptyString_ThrowsException()
	{
		// Arrange
		const string emptyString = "";

		// Act & Assert
		Assert.Throws<InvalidDomainValueException>(() => NonEmptyString.Validate(emptyString));
	}

	[Fact]
	public void NonEmptyString_Default_ReturnsDefaultValue()
	{
		// Arrange
		const string expectedDefaultValue = "N/A";

		// Act & Assert
		Assert.Equal(expectedDefaultValue, NonEmptyString.Default);
	}
}