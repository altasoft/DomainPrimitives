using Xunit;

namespace AltaSoft.DomainPrimitives.UnitTests.TransformableTests;

public class TransformableDomainPrimitivesTests
{
    [Fact]
    public void ToUpperString_ShouldConvertValueToUpperBeforeValidation()
    {
        var toUpper = new ToUpperString("abcd");
        Assert.Equal("ABCD", toUpper);

        var result = ToUpperString.TryCreate("abcd", out var toUpperResult);
        Assert.True(result);
        Assert.Equal("ABCD", toUpperResult);

    }
    [Fact]
    public void AbsoluteInt_ShouldConvertValuePositiveBeforeValidation()
    {
        var value = new AbsoluteInt(-1);
        Assert.Equal(1, (int)value);

        var result = AbsoluteInt.TryCreate(-1, out var toUpperResult);
        Assert.True(result);
        Assert.Equal(1, toUpperResult);

    }
}
