﻿using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace AltaSoft.DomainPrimitives.XmlDataTypes;

/// <summary>
/// Represents an XML gMonth value object, providing operations for parsing and handling gMonth values.
/// </summary>
/// <example>12</example>
[SerializationFormat("MM")]
[StructLayout(LayoutKind.Auto)]
public readonly partial struct GMonth : IDomainValue<DateOnly>
{
    /// <inheritdoc/>
    public static PrimitiveValidationResult Validate(DateOnly value) => PrimitiveValidationResult.Ok;

    /// <inheritdoc/>
    public static string ToString(DateOnly value) => value.ToString("MM", CultureInfo.InvariantCulture);
}
