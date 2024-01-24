﻿//HintName: ByteValueJsonConverter.g.cs
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
/// JsonConverter for <see cref = "ByteValue"/>
/// </summary>
public sealed class ByteValueJsonConverter : JsonConverter<ByteValue>
{
    /// <inheritdoc/>
    public override ByteValue Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        try
        {
            return JsonInternalConverters.ByteConverter.Read(ref reader, typeToConvert, options);
        }
        catch (InvalidDomainValueException ex)
        {
            throw new JsonException(ex.Message);
        }
    }

    /// <inheritdoc/>
    public override void Write(Utf8JsonWriter writer, ByteValue value, JsonSerializerOptions options)
    {
        JsonInternalConverters.ByteConverter.Write(writer, (byte)value, options);
    }

    /// <inheritdoc/>
    public override ByteValue ReadAsPropertyName(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        try
        {
            return JsonInternalConverters.ByteConverter.ReadAsPropertyName(ref reader, typeToConvert, options);
        }
        catch (InvalidDomainValueException ex)
        {
            throw new JsonException(ex.Message);
        }
    }

    /// <inheritdoc/>
    public override void WriteAsPropertyName(Utf8JsonWriter writer, ByteValue value, JsonSerializerOptions options)
    {
        JsonInternalConverters.ByteConverter.WriteAsPropertyName(writer, (byte)value, options);
    }
}
