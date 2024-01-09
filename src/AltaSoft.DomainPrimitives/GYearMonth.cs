using AltaSoft.DomainPrimitives.Abstractions;
using System;

namespace AltaSoft.DomainPrimitives;

/// <summary>
/// Represents an XML gYearMonth value object, providing operations for parsing and handling gYearMonth values.
/// </summary>
[SerializationFormat("yyyy-MM")]
public readonly partial record struct GYearMonth : IDomainValue<DateOnly>
{
	/// <inheritdoc/>
	public static void Validate(DateOnly value)
	{ }

	/// <inheritdoc/>
	public static DateOnly Default => default;

	/// <inheritdoc/>
	public static string ToString(DateOnly value) => value.ToString("yyyy-MM");
}