
using System.Text.Json.Serialization;

namespace AltaSoft.DomainPrimitives.Tests;

/// <summary>
/// This class contains unit tests for the JsonInternalConverters class.
/// </summary>
public class JsonInternalConvertersTests
{
	[Fact]
	public void SingleConverter_ReturnsJsonConverterForFloat()
	{
		var converter = JsonInternalConverters.SingleConverter;

		Assert.NotNull(converter);
		Assert.IsAssignableFrom<JsonConverter<float>>(converter);
	}

	[Fact]
	public void GuidConverter_ReturnsJsonConverterForGuid()
	{
		var converter = JsonInternalConverters.GuidConverter;

		Assert.NotNull(converter);
		Assert.IsAssignableFrom<JsonConverter<Guid>>(converter);
	}

	[Fact]
	public void BooleanConverter_ReturnsJsonConverterForBool()
	{
		var converter = JsonInternalConverters.BooleanConverter;

		Assert.NotNull(converter);
		Assert.IsAssignableFrom<JsonConverter<bool>>(converter);
	}

	[Fact]
	public void ByteConverter_ReturnsJsonConverterForByte()
	{
		var converter = JsonInternalConverters.ByteConverter;

		Assert.NotNull(converter);
		Assert.IsAssignableFrom<JsonConverter<byte>>(converter);
	}

	// Add more tests for other converters...

	[Fact]
	public void GetConverter_ReturnsJsonConverterForType()
	{
		var converter = JsonInternalConverters.GetConverter<int>();

		Assert.NotNull(converter);
		Assert.IsAssignableFrom<JsonConverter<int>>(converter);
	}
}