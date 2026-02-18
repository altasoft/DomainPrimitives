using System.Xml;
using Xunit;

namespace AltaSoft.DomainPrimitives.UnitTests;

public class DateOnlyConversionTests
{
    [Fact]
    public void ReadElementContentAsDateOnly_WithDateOnlyString_ReturnsDateOnly()
    {
        var xml = "<root><d>2024-04-01</d></root>";
        using var reader = XmlReader.Create(new StringReader(xml));
        reader.ReadToFollowing("d");

        var result = reader.ReadElementContentAsDateOnly();

        Assert.Equal(new DateOnly(2024, 4, 1), result);
    }

    [Fact]
    public void ReadElementContentAsDateOnly_WithTimezoneString_ReturnsDateOnly()
    {
        var xml = "<root><d>2024-04-01T12:34:56+03:00</d></root>";
        using var reader = XmlReader.Create(new System.IO.StringReader(xml));
        reader.ReadToFollowing("d");

        var result = reader.ReadElementContentAsDateOnly();

        Assert.Equal(new DateOnly(2024, 4, 1), result);
    }
}