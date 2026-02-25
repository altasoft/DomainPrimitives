using System.Xml;
using Xunit;

namespace AltaSoft.DomainPrimitives.UnitTests;

public class ParseTimeOnlyTests
{
    // ─── Valid cases ───────────────────────────────────────────────────────────

    [Theory]
    [InlineData("15:00:00", 15, 0, 0)]
    [InlineData("00:00:00", 0, 0, 0)]
    [InlineData("23:59:59", 23, 59, 59)]
    [InlineData("01:02:03", 1, 2, 3)]
    // With positive offset
    [InlineData("15:00:00+04:00", 15, 0, 0)]
    [InlineData("15:00:00+00:00", 15, 0, 0)]
    [InlineData("00:00:00+05:30", 0, 0, 0)]
    [InlineData("23:59:59+14:00", 23, 59, 59)]
    // With negative offset
    [InlineData("15:00:00-04:00", 15, 0, 0)]
    [InlineData("15:00:00-00:00", 15, 0, 0)]
    [InlineData("00:00:00-05:30", 0, 0, 0)]

    // With bare plus
    [InlineData("15:00:00+", 15, 0, 0)]
    [InlineData("00:00:00+", 0, 0, 0)]
    public void Parse_ValidInput_ReturnsExpectedTimeOnly(string input, int hour, int minute, int second)
    {
        var result = ParseTimeOnly(input);

        Assert.Equal(new TimeOnly(hour, minute, second), result);
    }

    // ─── Invalid cases: has date part ─────────────────────────────────────────

    [Theory]
    [InlineData("2025/01/11")]
    [InlineData("11/01/2025")]
    public void Parse_InputWithDatePart_Throws(string input)
    {
        Assert.Throws<FormatException>(() => ParseTimeOnly(input));
    }

    // ─── Invalid cases: malformed time ────────────────────────────────────────

    [Theory]
    [InlineData("99:00:00")]        // invalid hour
    [InlineData("15:60:00")]        // invalid minute
    [InlineData("15:00:60")]        // invalid second
    [InlineData("abc")]             // garbage
    [InlineData("1500:00")]         // malformed
    [InlineData("15:00:00++04:00")] // double operator
    [InlineData("15:00:00+25:00")]  // invalid offset hour
    [InlineData("")]                // empty
    [InlineData("   ")]             // whitespace
    public void Parse_MalformedInput_Throws(string input)
    {
        Assert.Throws<FormatException>(() => ParseTimeOnly(input));
    }
    private static TimeOnly ParseTimeOnly(string content)
    {
        var xml = $"<root>{content}</root>";
        var reader = XmlReader.Create(new StringReader(xml));
        reader.ReadToFollowing("root");
        return reader.ReadElementContentAsTimeOnly();
    }
}
