using System;
using System.Globalization;

namespace AltaSoft.DomainPrimitives.XmlDataTypes;

/// <summary>
/// Represents an XML gMonth value object, providing operations for parsing and handling gMonth values.
/// </summary>
/// <example>12</example>
[SerializationFormat("MM")]
public readonly partial struct GMonth : IDomainValue<DateOnly>
{
    /// <inheritdoc/>
    public static void Validate(DateOnly value)
    { }

    /// <inheritdoc/>
    public static DateOnly Default => default;

    /// <inheritdoc/>
    public static string ToString(DateOnly value) => value.ToString("MM", CultureInfo.InvariantCulture);
}
