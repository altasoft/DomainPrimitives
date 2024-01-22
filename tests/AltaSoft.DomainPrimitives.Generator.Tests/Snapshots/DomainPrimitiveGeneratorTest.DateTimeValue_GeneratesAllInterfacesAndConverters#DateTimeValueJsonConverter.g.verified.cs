﻿//HintName: DateTimeValueJsonConverter.g.cs
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
/// JsonConverter for <see cref = "DateTimeValue"/>
/// </summary>
public sealed class DateTimeValueJsonConverter : JsonConverter<DateTimeValue>
{
	/// <inheritdoc/>
	public override DateTimeValue Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		try
		{
			return JsonInternalConverters.DateTimeConverter.Read(ref reader, typeToConvert, options);
		}
		catch (InvalidDomainValueException ex)
		{
			throw new JsonException(ex.Message);
		}
	}

	/// <inheritdoc/>
	public override void Write(Utf8JsonWriter writer, DateTimeValue value, JsonSerializerOptions options)
	{
		JsonInternalConverters.DateTimeConverter.Write(writer, (DateTime)value, options);
	}

	/// <inheritdoc/>
	public override DateTimeValue ReadAsPropertyName(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		try
		{
			return JsonInternalConverters.DateTimeConverter.ReadAsPropertyName(ref reader, typeToConvert, options);
		}
		catch (InvalidDomainValueException ex)
		{
			throw new JsonException(ex.Message);
		}
	}

	/// <inheritdoc/>
	public override void WriteAsPropertyName(Utf8JsonWriter writer, DateTimeValue value, JsonSerializerOptions options)
	{
		JsonInternalConverters.DateTimeConverter.WriteAsPropertyName(writer, (DateTime)value, options);
	}
}
