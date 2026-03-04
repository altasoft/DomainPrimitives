using Xunit;

namespace AltaSoft.DomainPrimitives.UnitTests.TransformableTests;

public class PatternBasedStringTests
{
    [Theory]
    [InlineData("ABCDE")]
    [InlineData("Hebboo")]
    [InlineData("ABCDEFGH")]
    [InlineData("ABCDEFGHAB")]
    public void Constructor_WithValidValue_CreatesInstance(string value)
    {
        var result = new PatternBasedString(value);

        Assert.NotNull(result);
        Assert.Equal(value, (string)result);
    }

    [Theory]
    [InlineData("ABC")]
    [InlineData("ABCD")]
    public void Constructor_WithValueTooShort_ThrowsInvalidDomainValueException(string value)
    {
        var exception = Assert.Throws<InvalidDomainValueException>(() => new PatternBasedString(value));

        Assert.NotNull(exception);
    }

    [Theory]
    [InlineData("ABCDEFGHIJK")]
    [InlineData("ABCDEFGHIJKLMNOP")]
    public void Constructor_WithValueTooLong_ThrowsInvalidDomainValueException(string value)
    {
        var exception = Assert.Throws<InvalidDomainValueException>(() => new PatternBasedString(value));

        Assert.NotNull(exception);
    }

    [Theory]
    [InlineData("ABCDI")]
    [InlineData("ABCDJ")]
    [InlineData("ABCDZ")]
    [InlineData("HELLO1")]
    [InlineData("ABC@E")]
    [InlineData("ABC DE")]
    public void Constructor_WithInvalidCharacters_ThrowsInvalidDomainValueException(string value)
    {
        var exception = Assert.Throws<InvalidDomainValueException>(() => new PatternBasedString(value));

        Assert.NotNull(exception);
    }

    [Theory]
    [InlineData("ABCDE")]
    [InlineData("CEBBAx")]
    [InlineData("ABCDEFGH")]
    public void TryCreate_WithValidValue_ReturnsTrue(string value)
    {
        var result = PatternBasedString.TryCreate(value, out var patternString);

        Assert.True(result);
        Assert.NotNull(patternString);
        Assert.Equal(value, (string)patternString);
    }

    [Theory]
    [InlineData("ABC")]
    [InlineData("ABCDEFGHIJK")]
    [InlineData("1abcde")]
    [InlineData("ABCDI")]
    [InlineData("ABC@E")]
    public void TryCreate_WithInvalidValue_ReturnsFalse(string value)
    {
        var result = PatternBasedString.TryCreate(value, out var patternString);

        Assert.False(result);
        Assert.Null(patternString);
    }

    [Fact]
    public void ImplicitCast_FromValidString_CreatesInstance()
    {
        PatternBasedString result = "aabccH";

        Assert.NotNull(result);
        Assert.Equal("aabccH", (string)result);
    }

    [Fact]
    public void ImplicitCast_FromInvalidString_ThrowsInvalidDomainValueException()
    {
        Assert.Throws<InvalidDomainValueException>(() =>
        {
            PatternBasedString result = "1invalid";
        });
    }

    [Fact]
    public void ImplicitCast_FromNull_ReturnsNull()
    {
        PatternBasedString? result = (string?)null;

        Assert.Null(result);
    }

}
