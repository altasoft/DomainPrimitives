﻿//HintName: DateTimeOffsetValueJsonConverter.g.cs
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by 'AltaSoft DomainPrimitives Generator'.
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
/// JsonConverter for <see cref = "DateTimeOffsetValue"/>
/// </summary>
public sealed class DateTimeOffsetValueJsonConverter : JsonConverter<DateTimeOffsetValue>
{
    /// <inheritdoc/>
    public override DateTimeOffsetValue Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        try
        {
            return JsonInternalConverters.DateTimeOffsetConverter.Read(ref reader, typeToConvert, options);
        }
        catch (InvalidDomainValueException ex)
        {
            throw new JsonException(ex.Message);
        }
    }

    /// <inheritdoc/>
    public override void Write(Utf8JsonWriter writer, DateTimeOffsetValue value, JsonSerializerOptions options)
    {
        JsonInternalConverters.DateTimeOffsetConverter.Write(writer, (DateTimeOffset)value, options);
    }

    /// <inheritdoc/>
    public override DateTimeOffsetValue ReadAsPropertyName(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        try
        {
            return JsonInternalConverters.DateTimeOffsetConverter.ReadAsPropertyName(ref reader, typeToConvert, options);
        }
        catch (InvalidDomainValueException ex)
        {
            throw new JsonException(ex.Message);
        }
    }

    /// <inheritdoc/>
    public override void WriteAsPropertyName(Utf8JsonWriter writer, DateTimeOffsetValue value, JsonSerializerOptions options)
    {
        JsonInternalConverters.DateTimeOffsetConverter.WriteAsPropertyName(writer, (DateTimeOffset)value, options);
    }
}
