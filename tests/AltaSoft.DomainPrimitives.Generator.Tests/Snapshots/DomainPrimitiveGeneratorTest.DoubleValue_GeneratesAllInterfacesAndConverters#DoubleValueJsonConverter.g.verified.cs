﻿//HintName: DoubleValueJsonConverter.g.cs
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
/// JsonConverter for <see cref = "DoubleValue"/>
/// </summary>
internal sealed class DoubleValueJsonConverter : JsonConverter<DoubleValue>
{
    /// <inheritdoc/>
    public override DoubleValue Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        try
        {
            return JsonInternalConverters.DoubleConverter.Read(ref reader, typeToConvert, options);
        }
        catch (InvalidDomainValueException ex)
        {
            throw new JsonException(ex.Message);
        }
    }

    /// <inheritdoc/>
    public override void Write(Utf8JsonWriter writer, DoubleValue value, JsonSerializerOptions options)
    {
        JsonInternalConverters.DoubleConverter.Write(writer, (double)value, options);
    }

    /// <inheritdoc/>
    public override DoubleValue ReadAsPropertyName(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        try
        {
            return JsonInternalConverters.DoubleConverter.ReadAsPropertyName(ref reader, typeToConvert, options);
        }
        catch (InvalidDomainValueException ex)
        {
            throw new JsonException(ex.Message);
        }
    }

    /// <inheritdoc/>
    public override void WriteAsPropertyName(Utf8JsonWriter writer, DoubleValue value, JsonSerializerOptions options)
    {
        JsonInternalConverters.DoubleConverter.WriteAsPropertyName(writer, (double)value, options);
    }
}
