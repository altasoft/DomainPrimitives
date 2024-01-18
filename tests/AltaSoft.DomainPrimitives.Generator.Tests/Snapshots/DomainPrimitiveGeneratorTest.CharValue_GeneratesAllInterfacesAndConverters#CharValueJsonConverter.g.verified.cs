﻿//HintName: CharValueJsonConverter.g.cs
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
/// JsonConverter for <see cref = "CharValue"/>
/// </summary>
public sealed class CharValueJsonConverter : JsonConverter<CharValue>
{
	/// <inheritdoc/>
	public override CharValue Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		try
		{
			return JsonInternalConverters.CharConverter.Read(ref reader, typeToConvert, options);
		}
		catch (InvalidDomainValueException ex)
		{
			throw new JsonException(ex.Message);
		}
	}

	/// <inheritdoc/>
	public override void Write(Utf8JsonWriter writer, CharValue value, JsonSerializerOptions options)
	{
		JsonInternalConverters.CharConverter.Write(writer, (char)value, options);
	}

	/// <inheritdoc/>
	public override CharValue ReadAsPropertyName(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		try
		{
			return JsonInternalConverters.CharConverter.ReadAsPropertyName(ref reader, typeToConvert, options);
		}
		catch (InvalidDomainValueException ex)
		{
			throw new JsonException(ex.Message);
		}
	}

	/// <inheritdoc/>
	public override void WriteAsPropertyName(Utf8JsonWriter writer, CharValue value, JsonSerializerOptions options)
	{
		JsonInternalConverters.CharConverter.WriteAsPropertyName(writer, (char)value, options);
	}
}
