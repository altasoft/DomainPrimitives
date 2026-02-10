using System.ComponentModel;
using System.Text.Json;
using System.Xml.Serialization;
using Xunit;

namespace AltaSoft.DomainPrimitives.UnitTests.SerializationFormatTests;

public class SerializationFormatTests
{
    [Fact]
    public void CustomDateTime_JsonSerialization_GeneratesCorrectValue()
    {
        var dateTime = new DateTime(2024, 1, 15, 14, 30, 45);
        var customDateTime = new CustomDateTime(dateTime);

        var json = JsonSerializer.Serialize(customDateTime);

        Assert.Equal("\"20240115_143045\"", json);
    }

    [Fact]
    public void CustomDateOnly_JsonSerialization_GeneratesCorrectValue()
    {
        var dateOnly = new DateOnly(2024, 1, 15);
        var customDateOnly = new CustomDateOnly(dateOnly);

        var json = JsonSerializer.Serialize(customDateOnly);

        Assert.Equal("\"20240115\"", json);
    }

    [Fact]
    public void CustomTimeOnly_JsonSerialization_GeneratesCorrectValue()
    {
        var timeOnly = new TimeOnly(14, 30, 45);
        var customTimeOnly = new CustomTimeOnly(timeOnly);

        var json = JsonSerializer.Serialize(customTimeOnly);

        Assert.Equal("\"143045\"", json);
    }

    [Fact]
    public void CustomDateTime_JsonDeserialization_Works()
    {
        const string json = "\"20240115_143045\"";

        var result = JsonSerializer.Deserialize<CustomDateTime>(json);

        Assert.Equal(new DateTime(2024, 1, 15, 14, 30, 45), result);
    }

    [Fact]
    public void CustomDateOnly_JsonDeserialization_Works()
    {
        const string json = "\"20240115\"";

        var result = JsonSerializer.Deserialize<CustomDateOnly>(json);

        Assert.Equal(new DateOnly(2024, 1, 15), (DateOnly)result);
    }

    [Fact]
    public void CustomTimeOnly_JsonDeserialization_Works()
    {
        var json = "\"143045\"";

        var result = JsonSerializer.Deserialize<CustomTimeOnly>(json);

        Assert.Equal(new TimeOnly(14, 30, 45), (TimeOnly)result);
    }

    [Fact]
    public void CustomDateTime_TypeConverter_Works()
    {
        var converter = TypeDescriptor.GetConverter(typeof(CustomDateTime));

        var result = converter.ConvertFrom("20240115_143045") as CustomDateTime?;

        Assert.Equal(new DateTime(2024, 1, 15, 14, 30, 45), (DateTime?)result);
    }

    [Fact]
    public void CustomDateOnly_TypeConverter_Works()
    {
        var converter = TypeDescriptor.GetConverter(typeof(CustomDateOnly));

        var result = converter.ConvertFrom("20240115") as CustomDateOnly?;

        Assert.Equal(new DateOnly(2024, 1, 15), (DateOnly?)result);
    }

    [Fact]
    public void CustomTimeOnly_TypeConverter_Works()
    {
        var converter = TypeDescriptor.GetConverter(typeof(CustomTimeOnly));

        var result = converter.ConvertFrom("143045") as CustomTimeOnly?;

        Assert.Equal(new TimeOnly(14, 30, 45), (TimeOnly?)result);
    }

    [Fact]
    public void CustomDateTime_XmlSerialization_Works()
    {
        var dateTime = new DateTime(2024, 1, 15, 14, 30, 45);
        var customDateTime = new CustomDateTime(dateTime);
        var serializer = new XmlSerializer(typeof(CustomDateTime));
        using var writer = new StringWriter();

        serializer.Serialize(writer, customDateTime);
        var xml = writer.ToString();

        Assert.Contains("20240115_143045", xml);
    }

    [Fact]
    public void CustomDateOnly_XmlSerialization_Works()
    {
        var dateOnly = new DateOnly(2024, 1, 15);
        var customDateOnly = new CustomDateOnly(dateOnly);
        var serializer = new XmlSerializer(typeof(CustomDateOnly));
        using var writer = new StringWriter();

        serializer.Serialize(writer, customDateOnly);
        var xml = writer.ToString();

        Assert.Contains("20240115", xml);
    }

    [Fact]
    public void CustomTimeOnly_XmlSerialization_Works()
    {
        var timeOnly = new TimeOnly(14, 30, 45);
        var customTimeOnly = new CustomTimeOnly(timeOnly);
        var serializer = new XmlSerializer(typeof(CustomTimeOnly));
        using var writer = new StringWriter();

        serializer.Serialize(writer, customTimeOnly);
        var xml = writer.ToString();

        Assert.Contains("143045", xml);
    }

    [Fact]
    public void CustomDateTime_XmlDeserialization_Works()
    {
        const string xml = """
                           <?xml version="1.0" encoding="utf-16"?>
                           <CustomDateTime>20240115_143045</CustomDateTime>
                           """;
        var serializer = new XmlSerializer(typeof(CustomDateTime));
        using var reader = new StringReader(xml);

        var result = serializer.Deserialize(reader) as CustomDateTime?;

        Assert.Equal(new DateTime(2024, 1, 15, 14, 30, 45), (DateTime?)result);
    }

    [Fact]
    public void CustomDateOnly_XmlDeserialization_Works()
    {
        const string xml = """
                           <?xml version="1.0" encoding="utf-16"?>
                           <CustomDateOnly>20240115</CustomDateOnly>
                           """;
        var serializer = new XmlSerializer(typeof(CustomDateOnly));
        using var reader = new StringReader(xml);

        var result = serializer.Deserialize(reader) as CustomDateOnly?;

        Assert.Equal(new DateOnly(2024, 1, 15), (DateOnly?)result);
    }

    [Fact]
    public void CustomTimeOnly_XmlDeserialization_Works()
    {
        const string xml = """
                           <?xml version="1.0" encoding="utf-16"?>
                           <CustomTimeOnly>143045</CustomTimeOnly>
                           """;
        var serializer = new XmlSerializer(typeof(CustomTimeOnly));
        using var reader = new StringReader(xml);

        var result = serializer.Deserialize(reader) as CustomTimeOnly?;

        Assert.Equal(new TimeOnly(14, 30, 45), result);
    }

    [Fact]
    public void CustomTimespan_JsonSerialization_GeneratesCorrectValue()
    {
        var timespan = new TimeSpan(14, 30, 0);
        var customTimespan = new CustomTimeSpan(timespan);

        var json = JsonSerializer.Serialize(customTimespan);

        Assert.Equal("\"14:30\"", json);
    }

    [Fact]
    public void CustomTimespan_JsonDeserialization_Works()
    {
        const string json = "\"14:30\"";

        var result = JsonSerializer.Deserialize<CustomTimeSpan>(json);

        Assert.Equal(new TimeSpan(14, 30, 0), (TimeSpan)result);
    }

    [Fact]
    public void CustomTimespan_TypeConverter_Works()
    {
        var converter = TypeDescriptor.GetConverter(typeof(CustomTimeSpan));

        var result = converter.ConvertFrom("14:30") as CustomTimeSpan?;

        Assert.Equal(new TimeSpan(14, 30, 0), (TimeSpan?)result);
    }

    [Fact]
    public void CustomTimespan_XmlSerialization_Works()
    {
        var timespan = new TimeSpan(14, 30, 0);
        var customTimespan = new CustomTimeSpan(timespan);
        var serializer = new XmlSerializer(typeof(CustomTimeSpan));
        using var writer = new StringWriter();

        serializer.Serialize(writer, customTimespan);
        var xml = writer.ToString();

        Assert.Contains("14:30", xml);
    }

    [Fact]
    public void CustomTimespan_XmlDeserialization_Works()
    {
        const string xml = """
                           <?xml version="1.0" encoding="utf-16"?>
                           <CustomTimeSpan>14:30</CustomTimeSpan>
                           """;
        var serializer = new XmlSerializer(typeof(CustomTimeSpan));
        using var reader = new StringReader(xml);

        var result = serializer.Deserialize(reader) as CustomTimeSpan?;

        Assert.Equal(new TimeSpan(14, 30, 0), (TimeSpan?)result);
    }

    [Fact]
    public void CustomDateTimeOffset_JsonSerialization_GeneratesCorrectValue()
    {
        var dateTimeOffset = new DateTimeOffset(2024, 1, 15, 0, 0, 0, TimeSpan.Zero);
        var customDateTimeOffset = new CustomDateTimeOffset(dateTimeOffset);

        var json = JsonSerializer.Serialize(customDateTimeOffset);

        Assert.Equal("\"20240115\"", json);
    }

    [Fact]
    public void CustomDateTimeOffset_JsonDeserialization_Works()
    {
        const string json = "\"20240115\"";

        var result = JsonSerializer.Deserialize<CustomDateTimeOffset>(json);

        var localOffset = TimeZoneInfo.Local.GetUtcOffset(new DateTime(2024, 1, 15));

        Assert.Equal(new DateTimeOffset(2024, 1, 15, 0, 0, 0, localOffset), result);
    }

    [Fact]
    public void CustomDateTimeOffset_TypeConverter_Works()
    {
        var converter = TypeDescriptor.GetConverter(typeof(CustomDateTimeOffset));

        var result = converter.ConvertFrom("20240115") as CustomDateTimeOffset?;
        var localOffset = TimeZoneInfo.Local.GetUtcOffset(new DateTime(2024, 1, 15));
        Assert.Equal(new DateTimeOffset(2024, 1, 15, 0, 0, 0, localOffset), (DateTimeOffset?)result);
    }

    [Fact]
    public void CustomDateTimeOffset_XmlSerialization_Works()
    {
        var dateTimeOffset = new DateTimeOffset(2024, 1, 15, 0, 0, 0, TimeSpan.Zero);
        var customDateTimeOffset = new CustomDateTimeOffset(dateTimeOffset);
        var serializer = new XmlSerializer(typeof(CustomDateTimeOffset));
        using var writer = new StringWriter();

        serializer.Serialize(writer, customDateTimeOffset);
        var xml = writer.ToString();

        Assert.Contains("20240115", xml);
    }

    [Fact]
    public void CustomDateTimeOffset_XmlDeserialization_Works()
    {
        const string xml = """
                           <?xml version="1.0" encoding="utf-16"?>
                           <CustomDateTimeOffset>20240115</CustomDateTimeOffset>
                           """;
        var serializer = new XmlSerializer(typeof(CustomDateTimeOffset));
        using var reader = new StringReader(xml);

        var result = serializer.Deserialize(reader) as CustomDateTimeOffset?;

        var localOffset = TimeZoneInfo.Local.GetUtcOffset(new DateTime(2024, 1, 15));

        Assert.Equal(new DateTimeOffset(2024, 1, 15, 0, 0, 0, localOffset), (DateTimeOffset?)result);
    }
}
