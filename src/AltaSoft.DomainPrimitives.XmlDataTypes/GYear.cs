using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace AltaSoft.DomainPrimitives.XmlDataTypes;

/// <summary>
/// Represents an XML gYear value object, providing operations for parsing and handling gYear values.
/// </summary>
/// <example>2024</example>
[SerializationFormat("yyyy")]
[StructLayout(LayoutKind.Auto)]
public readonly partial struct GYear : IDomainValue<DateOnly>
{
    /// <inheritdoc/>
    public static PrimitiveValidationResult Validate(DateOnly value) => PrimitiveValidationResult.Ok;

    /// <inheritdoc/>
    public static string ToString(DateOnly value) => value.ToString("yyyy", CultureInfo.InvariantCulture);
}
