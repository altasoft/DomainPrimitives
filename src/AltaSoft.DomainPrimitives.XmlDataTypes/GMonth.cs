using System;

namespace AltaSoft.DomainPrimitives;

/// <summary>
/// Represents an XML gMonth value object, providing operations for parsing and handling gMonth values.
/// </summary>
[SerializationFormat("MM")]
public readonly partial struct GMonth : IDomainValue<DateOnly>
{
	/// <inheritdoc/>
	public static void Validate(DateOnly value)
	{ }

	/// <inheritdoc/>
	public static DateOnly Default => default;

	/// <inheritdoc/>
	public static string ToString(DateOnly value) => value.ToString("MM");
}