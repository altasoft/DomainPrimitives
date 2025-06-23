using System.Xml;

namespace AltaSoft.DomainPrimitives.XmlDataTypes.Tests;

public class ToXmlStringExtTests
{
    [Fact]
    public void DateTime_ToXmlString_ReturnsExpectedFormat()
    {
        var dt = new DateTime(2024, 4, 1, 13, 45, 30, DateTimeKind.Local);
        var xml = dt.ToXmlString();
        Assert.Equal("2024-04-01T13:45:30+04:00", xml);
    }

    [Fact]
    public void DateOnly_ToXmlString_ReturnsExpectedFormat()
    {
        var d = new DateOnly(2024, 4, 1);
        Assert.Equal("2024-04-01", d.ToXmlString());
    }

    [Fact]
    public void TimeOnly_ToXmlString_ReturnsExpectedFormat()
    {
        var t = new TimeOnly(13, 45, 30);
        var xml = t.ToXmlString();
        Assert.Equal("13:45:30", xml);
    }

    [Fact]
    public void DateTimeOffset_ToXmlString_ReturnsXmlConvertString()
    {
        var dto = new DateTimeOffset(2024, 4, 1, 13, 45, 30, TimeSpan.Zero);
        Assert.Equal(XmlConvert.ToString(dto), dto.ToXmlString());
    }

    [Fact]
    public void TimeSpan_ToXmlString_ReturnsXmlConvertString()
    {
        var ts = new TimeSpan(1, 2, 3);
        Assert.Equal(XmlConvert.ToString(ts), ts.ToXmlString());
    }

    [Theory]
    [InlineData((byte)42)]
    [InlineData((sbyte)-42)]
    [InlineData((short)-1234)]
    [InlineData((ushort)1234)]
    [InlineData(123456)]
    [InlineData(123456U)]
    [InlineData(1234567890123L)]
    [InlineData(1234567890123UL)]
    [InlineData(3.14f)]
    [InlineData(2.71828)]
    public void NumericTypes_ToXmlString_UsesInvariantCulture(object value)
    {
        var xml = value switch
        {
            byte b => b.ToXmlString(),
            sbyte sb => sb.ToXmlString(),
            short s => s.ToXmlString(),
            ushort us => us.ToXmlString(),
            int i => i.ToXmlString(),
            uint ui => ui.ToXmlString(),
            long l => l.ToXmlString(),
            ulong ul => ul.ToXmlString(),
            float f => f.ToXmlString(),
            double d => d.ToXmlString(),
            _ => throw new NotSupportedException()
        };
        Assert.DoesNotContain(',', xml); // Invariant culture uses '.'
    }

    [Fact]
    public void Decimal_ToXmlString_UsesInvariantCulture()
    {
        const decimal m = 123.456m;
        var xml = m.ToXmlString();
        Assert.DoesNotContain(',', xml);
    }

    [Fact]
    public void Guid_ToXmlString_ReturnsToString()
    {
        var guid = Guid.NewGuid();
        Assert.Equal(guid.ToString(), guid.ToXmlString());
    }

    [Theory]
    [InlineData(true, "true")]
    [InlineData(false, "false")]
    public void Bool_ToXmlString_ReturnsExpected(bool value, string expected)
    {
        Assert.Equal(expected, value.ToXmlString());
    }

    [Fact]
    public void Char_ToXmlString_ReturnsToString()
    {
        Assert.Equal("A", 'A'.ToXmlString());
    }
}
