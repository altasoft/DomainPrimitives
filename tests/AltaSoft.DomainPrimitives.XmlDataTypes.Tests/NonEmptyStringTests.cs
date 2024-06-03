namespace AltaSoft.DomainPrimitives.XmlDataTypes.Tests;

/// <summary>
/// Contains unit tests for the NonEmptyString class.
/// </summary>
public class NonEmptyStringTests
{
    [Fact]
    public void NonEmptyString_Validate_ValidString_ShouldBe_Valid()
    {
        // Arrange
        const string validString = "Hello";

        // Act & Assert
        Assert.True(AsciiString.Validate(validString).IsValid);
    }

    [Fact]
    public void NonEmptyString_Validate_NullString_ThrowsException()
    {
        // Arrange
        const string? nullString = null;

        // Act & Assert
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        Assert.False(NonEmptyString.Validate(nullString).IsValid);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
    }

    [Fact]
    public void NonEmptyString_Validate_EmptyString_ThrowsException()
    {
        // Arrange
        const string emptyString = "";

        // Act & Assert
        Assert.False(NonEmptyString.Validate(emptyString).IsValid);
    }
}
