﻿//HintName: BoolValueJsonConverter.g.cs
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
/// JsonConverter for <see cref = "BoolValue"/>
/// </summary>
internal sealed class BoolValueJsonConverter : JsonConverter<BoolValue>
{
    /// <inheritdoc/>
    public override BoolValue Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        try
        {
            return JsonInternalConverters.BooleanConverter.Read(ref reader, typeToConvert, options);
        }
        catch (InvalidDomainValueException ex)
        {
            throw new JsonException(ex.Message);
        }
    }

    /// <inheritdoc/>
    public override void Write(Utf8JsonWriter writer, BoolValue value, JsonSerializerOptions options)
    {
        JsonInternalConverters.BooleanConverter.Write(writer, (bool)value, options);
    }

    /// <inheritdoc/>
    public override BoolValue ReadAsPropertyName(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        try
        {
            return JsonInternalConverters.BooleanConverter.ReadAsPropertyName(ref reader, typeToConvert, options);
        }
        catch (InvalidDomainValueException ex)
        {
            throw new JsonException(ex.Message);
        }
    }

    /// <inheritdoc/>
    public override void WriteAsPropertyName(Utf8JsonWriter writer, BoolValue value, JsonSerializerOptions options)
    {
        JsonInternalConverters.BooleanConverter.WriteAsPropertyName(writer, (bool)value, options);
    }
}
