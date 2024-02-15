using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace AltaSoft.DomainPrimitives.XmlDataTypes;

/// <summary>
/// Represents an XML gYearMonth value object, providing operations for parsing and handling gYearMonth values.
/// </summary>
/// <example>2024-12</example>
[SerializationFormat("yyyy-MM")]
[StructLayout(LayoutKind.Auto)]
public readonly partial struct GYearMonth : IDomainValue<DateOnly>
{
    /// <inheritdoc/>
    public static void Validate(DateOnly value)
    { }

    /// <inheritdoc/>
    public static DateOnly Default => default;

    /// <inheritdoc/>
    public static string ToString(DateOnly value) => value.ToString("yyyy-MM", CultureInfo.InvariantCulture);
}
