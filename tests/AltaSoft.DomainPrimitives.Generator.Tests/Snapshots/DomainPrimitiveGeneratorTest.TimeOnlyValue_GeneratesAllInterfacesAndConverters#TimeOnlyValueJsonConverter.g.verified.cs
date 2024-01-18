﻿//HintName: TimeOnlyValueJsonConverter.g.cs
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
using AltaSoft.DomainPrimitives.Abstractions;

namespace AltaSoft.DomainPrimitives.Converters;

/// <summary>
/// JsonConverter for <see cref = "TimeOnlyValue"/>
/// </summary>
public sealed class TimeOnlyValueJsonConverter : JsonConverter<TimeOnlyValue>
{
	/// <inheritdoc/>
	public override TimeOnlyValue Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		try
		{
			return JsonInternalConverters.TimeOnlyConverter.Read(ref reader, typeToConvert, options);
		}
		catch (InvalidDomainValueException ex)
		{
			throw new JsonException(ex.Message);
		}
	}

	/// <inheritdoc/>
	public override void Write(Utf8JsonWriter writer, TimeOnlyValue value, JsonSerializerOptions options)
	{
		JsonInternalConverters.TimeOnlyConverter.Write(writer, (TimeOnly)value, options);
	}

	/// <inheritdoc/>
	public override TimeOnlyValue ReadAsPropertyName(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		try
		{
			return JsonInternalConverters.TimeOnlyConverter.ReadAsPropertyName(ref reader, typeToConvert, options);
		}
		catch (InvalidDomainValueException ex)
		{
			throw new JsonException(ex.Message);
		}
	}

	/// <inheritdoc/>
	public override void WriteAsPropertyName(Utf8JsonWriter writer, TimeOnlyValue value, JsonSerializerOptions options)
	{
		JsonInternalConverters.TimeOnlyConverter.WriteAsPropertyName(writer, (TimeOnly)value, options);
	}
}
