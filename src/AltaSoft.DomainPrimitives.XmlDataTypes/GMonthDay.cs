using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace AltaSoft.DomainPrimitives.XmlDataTypes;

/// <summary>
/// Represents an XML gMonthDay value object, providing operations for parsing and handling gMonthDay values.
/// </summary>
/// <example>12-31</example>
[SerializationFormat("MM-dd")]
[StructLayout(LayoutKind.Auto)]
public readonly partial struct GMonthDay : IDomainValue<DateOnly>
{
    /// <inheritdoc/>
    public static PrimitiveValidationResult Validate(DateOnly value) => PrimitiveValidationResult.Ok;

    /// <inheritdoc/>
    public static string ToString(DateOnly value) => value.ToString("MM-dd", CultureInfo.InvariantCulture);
}
