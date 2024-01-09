using AltaSoft.DomainPrimitives.Abstractions;

namespace AltaSoft.DomainPrimitives.Tests;

/// <summary>
/// Contains unit tests for the AsciiString class.
/// </summary>
public class AsciiStringTests
{
	[Fact]
	public void Validate_ValidAsciiString_DoesNotThrowException()
	{
		// Arrange
		const string validString = "Hello";

		// Act & Assert
		var exception = Record.Exception(() => AsciiString.Validate(validString));
		Assert.Null(exception);
	}

	[Fact]
	public void Validate_InvalidAsciiString_ThrowsException()
	{
		// Arrange
		const string invalidString = "Hello😊";

		// Act & Assert
		Assert.Throws<InvalidDomainValueException>(() => AsciiString.Validate(invalidString));
	}

	[Fact]
	public void Default_ReturnsEmptyString()
	{
		// Act
		var defaultValue = AsciiString.Default;

		// Assert
		Assert.Equal(string.Empty, defaultValue);
	}
}