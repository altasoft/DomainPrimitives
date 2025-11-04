using Xunit;

namespace AltaSoft.DomainPrimitives.UnitTests.TransformableTests;

public class NestedDomainPrimitivesTests
{
    [Fact]
    public void CanCreateNestedString_FromToUpperString()
    {
        var toUpper = new ToUpperString("HELLO");
        var nested = new NestedString(toUpper);
        Assert.NotNull(nested);
        Assert.Equal("HELLO", nested);

        var nested2 = new NestedString("HELLO");
        Assert.Equal(nested, nested2);
    }

    [Fact]
    public void CanCreateNestedString_FromString_ImplicitCast()
    {
        NestedString nested = "HELLO";
        Assert.NotNull(nested);

        NestedString? nestedNull = (string?)null;
        Assert.Null(nestedNull);
    }

    [Fact]
    public void CanCreateNestedString_FromToUpperString_ImplicitCast()
    {
        ToUpperString toUpper = "HELLO";
        NestedString nested = toUpper;
        Assert.NotNull(nested);

        NestedString? nestedNull = (ToUpperString?)null;
        Assert.Null(nestedNull);
    }

    [Fact]
    public void CreatingNestedString_WithError_Throws()
    {
        ToUpperString toUpper = "ErrorValue";
        Assert.Throws<InvalidDomainValueException>(() => new NestedString(toUpper));
    }

    [Fact]
    public void CreatingNestedString_FromStringWithError_Throws()
    {
        Assert.Throws<InvalidDomainValueException>(() => { NestedString _ = "ErrorValue"; });
    }

    [Fact]
    public void CanCreateNestedInt_FromAbsoluteInt()
    {
        var abs = new AbsoluteInt(5);
        var nested = new NestedInt(abs);
        Assert.Equal(5, (int)nested);

        var nested2 = new NestedInt(5);
        Assert.Equal(nested, nested2);
    }

    [Fact]
    public void CanCreateNestedInt_FromInt_ImplicitCast()
    {
        NestedInt nested = 5;
        Assert.Equal(5, (int)nested);

        NestedInt? nestedNull = (int?)null;
        Assert.Null(nestedNull);
    }

    [Fact]
    public void CanCreateNestedInt_FromAbsoluteInt_ImplicitCast()
    {
        AbsoluteInt abs = 5;
        NestedInt nested = abs;
        Assert.Equal(5, (int)nested);

        NestedInt? nestedNull = (AbsoluteInt?)null;
        Assert.Null(nestedNull);
    }

    [Fact]
    public void CreatingNestedInt_WithError_Throws()
    {
        AbsoluteInt abs = 11;
        Assert.Throws<InvalidDomainValueException>(() => new NestedInt(abs));
    }

    [Fact]
    public void CreatingNestedInt_FromIntWithError_Throws()
    {
        Assert.Throws<InvalidDomainValueException>(() => { NestedInt n = 11; });
    }
}
