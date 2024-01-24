﻿//HintName: GuidValueJsonConverter.g.cs
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
/// JsonConverter for <see cref = "GuidValue"/>
/// </summary>
public sealed class GuidValueJsonConverter : JsonConverter<GuidValue>
{
    /// <inheritdoc/>
    public override GuidValue Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        try
        {
            return JsonInternalConverters.GuidConverter.Read(ref reader, typeToConvert, options);
        }
        catch (InvalidDomainValueException ex)
        {
            throw new JsonException(ex.Message);
        }
    }

    /// <inheritdoc/>
    public override void Write(Utf8JsonWriter writer, GuidValue value, JsonSerializerOptions options)
    {
        JsonInternalConverters.GuidConverter.Write(writer, (Guid)value, options);
    }

    /// <inheritdoc/>
    public override GuidValue ReadAsPropertyName(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        try
        {
            return JsonInternalConverters.GuidConverter.ReadAsPropertyName(ref reader, typeToConvert, options);
        }
        catch (InvalidDomainValueException ex)
        {
            throw new JsonException(ex.Message);
        }
    }

    /// <inheritdoc/>
    public override void WriteAsPropertyName(Utf8JsonWriter writer, GuidValue value, JsonSerializerOptions options)
    {
        JsonInternalConverters.GuidConverter.WriteAsPropertyName(writer, (Guid)value, options);
    }
}
