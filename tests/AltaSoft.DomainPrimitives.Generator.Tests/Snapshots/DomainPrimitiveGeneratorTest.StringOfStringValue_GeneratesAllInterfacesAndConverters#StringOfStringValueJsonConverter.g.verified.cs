﻿//HintName: StringOfStringValueJsonConverter.g.cs
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
/// JsonConverter for <see cref = "StringOfStringValue"/>
/// </summary>
public sealed class StringOfStringValueJsonConverter : JsonConverter<StringOfStringValue>
{
    /// <inheritdoc/>
    public override StringOfStringValue Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        try
        {
            return JsonInternalConverters.StringConverter.Read(ref reader, typeToConvert, options)!;
        }
        catch (InvalidDomainValueException ex)
        {
            throw new JsonException(ex.Message);
        }
    }

    /// <inheritdoc/>
    public override void Write(Utf8JsonWriter writer, StringOfStringValue value, JsonSerializerOptions options)
    {
        JsonInternalConverters.StringConverter.Write(writer, (string)value, options);
    }

    /// <inheritdoc/>
    public override StringOfStringValue ReadAsPropertyName(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        try
        {
            return JsonInternalConverters.StringConverter.ReadAsPropertyName(ref reader, typeToConvert, options)!;
        }
        catch (InvalidDomainValueException ex)
        {
            throw new JsonException(ex.Message);
        }
    }

    /// <inheritdoc/>
    public override void WriteAsPropertyName(Utf8JsonWriter writer, StringOfStringValue value, JsonSerializerOptions options)
    {
        JsonInternalConverters.StringConverter.WriteAsPropertyName(writer, (string)value, options);
    }
}
