﻿//HintName: TimeSpanValueJsonConverter.g.cs
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a AltaSoft.DomainPrimitives.Generator v1.0.0
//     Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

#nullable enable

using AltaSoft.DomainPrimitives;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Globalization;
using System.Text.Json.Serialization.Metadata;

namespace AltaSoft.DomainPrimitives.Converters;

/// <summary>
/// JsonConverter for <see cref = "TimeSpanValue"/>
/// </summary>
public sealed class TimeSpanValueJsonConverter : JsonConverter<TimeSpanValue>
{
	/// <inheritdoc/>
	public override TimeSpanValue Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		try
		{
			return JsonInternalConverters.TimeSpanConverter.Read(ref reader, typeToConvert, options);
		}
		catch (InvalidDomainValueException ex)
		{
			throw new JsonException(ex.Message);
		}
	}

	/// <inheritdoc/>
	public override void Write(Utf8JsonWriter writer, TimeSpanValue value, JsonSerializerOptions options)
	{
		JsonInternalConverters.TimeSpanConverter.Write(writer, (TimeSpan)value, options);
	}

	/// <inheritdoc/>
	public override TimeSpanValue ReadAsPropertyName(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		try
		{
			return JsonInternalConverters.TimeSpanConverter.ReadAsPropertyName(ref reader, typeToConvert, options);
		}
		catch (InvalidDomainValueException ex)
		{
			throw new JsonException(ex.Message);
		}
	}

	/// <inheritdoc/>
	public override void WriteAsPropertyName(Utf8JsonWriter writer, TimeSpanValue value, JsonSerializerOptions options)
	{
		JsonInternalConverters.TimeSpanConverter.WriteAsPropertyName(writer, (TimeSpan)value, options);
	}
}
