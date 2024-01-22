using System;
using System.Globalization;

namespace AltaSoft.DomainPrimitives.XmlDataTypes;

/// <summary>
/// Represents an XML gYear value object, providing operations for parsing and handling gYear values.
/// </summary>
/// <example>2024</example>
[SerializationFormat("yyyy")]
public readonly partial struct GYear : IDomainValue<DateOnly>
{
    /// <inheritdoc/>
    public static void Validate(DateOnly value)
    { }

    /// <inheritdoc/>
    public static DateOnly Default => default;

    /// <inheritdoc/>
    public static string ToString(DateOnly value) => value.ToString("yyyy", CultureInfo.InvariantCulture);
}
