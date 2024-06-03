using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace AltaSoft.DomainPrimitives.XmlDataTypes;

/// <summary>
/// Represents an XML gDay value object, providing operations for parsing and handling gDay values.
/// </summary>
/// <example>31</example>
[SerializationFormat("dd")]
[StructLayout(LayoutKind.Auto)]
public readonly partial struct GDay : IDomainValue<DateOnly>
{
    /// <inheritdoc/>
    public static PrimitiveValidationResult Validate(DateOnly value) => PrimitiveValidationResult.Ok;

    /// <inheritdoc/>
    public static string ToString(DateOnly value) => value.ToString("dd", CultureInfo.InvariantCulture);
}
