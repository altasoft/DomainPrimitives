using AltaSoft.DomainPrimitives.Abstractions;
using System;

namespace AltaSoft.DomainPrimitives;

/// <summary>
/// Represents an XML gYear value object, providing operations for parsing and handling gYear values.
/// </summary>
[SerializationFormat("yyyy")]
public readonly partial struct GYear : IDomainValue<DateOnly>
{
	/// <inheritdoc/>
	public static void Validate(DateOnly value)
	{ }

	/// <inheritdoc/>
	public static DateOnly Default => default;

	/// <inheritdoc/>
	public static string ToString(DateOnly value) => value.ToString("yyyy");
}