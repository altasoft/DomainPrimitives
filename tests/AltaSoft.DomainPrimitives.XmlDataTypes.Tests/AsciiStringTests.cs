﻿namespace AltaSoft.DomainPrimitives.XmlDataTypes.Tests;

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
        Assert.False(AsciiString.Validate(invalidString).IsValid);
    }
}
