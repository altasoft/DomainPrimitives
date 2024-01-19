using System;

namespace AltaSoft.DomainPrimitives;

/// <summary>
/// Represents an XML gMonthDay value object, providing operations for parsing and handling gMonthDay values.
/// </summary>
/// <example>12-31</example>
[SerializationFormat("MM-dd")]
public readonly partial struct GMonthDay : IDomainValue<DateOnly>
{
	/// <inheritdoc/>
	public static void Validate(DateOnly value)
	{ }

	/// <inheritdoc/>
	public static DateOnly Default => default;

	/// <inheritdoc/>
	public static string ToString(DateOnly value) => value.ToString("MM-dd");
}