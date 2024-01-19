﻿//HintName: SByteValueJsonConverter.g.cs
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
/// JsonConverter for <see cref = "SByteValue"/>
/// </summary>
public sealed class SByteValueJsonConverter : JsonConverter<SByteValue>
{
	/// <inheritdoc/>
	public override SByteValue Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		try
		{
			return JsonInternalConverters.SByteConverter.Read(ref reader, typeToConvert, options);
		}
		catch (InvalidDomainValueException ex)
		{
			throw new JsonException(ex.Message);
		}
	}

	/// <inheritdoc/>
	public override void Write(Utf8JsonWriter writer, SByteValue value, JsonSerializerOptions options)
	{
		JsonInternalConverters.SByteConverter.Write(writer, (sbyte)value, options);
	}

	/// <inheritdoc/>
	public override SByteValue ReadAsPropertyName(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		try
		{
			return JsonInternalConverters.SByteConverter.ReadAsPropertyName(ref reader, typeToConvert, options);
		}
		catch (InvalidDomainValueException ex)
		{
			throw new JsonException(ex.Message);
		}
	}

	/// <inheritdoc/>
	public override void WriteAsPropertyName(Utf8JsonWriter writer, SByteValue value, JsonSerializerOptions options)
	{
		JsonInternalConverters.SByteConverter.WriteAsPropertyName(writer, (sbyte)value, options);
	}
}