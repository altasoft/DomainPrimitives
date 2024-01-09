﻿using AltaSoft.DomainPrimitives.Abstractions;
using System;

namespace AltaSoft.DomainPrimitives;

/// <summary>
/// Represents an XML gDay value object, providing operations for parsing and handling gDay values.
/// </summary>
[SerializationFormat("dd")]
public readonly partial record struct GDay : IDomainValue<DateOnly>
{
	/// <inheritdoc/>
	public static void Validate(DateOnly value)
	{ }

	/// <inheritdoc/>
	public static DateOnly Default => default;

	/// <inheritdoc/>
	public static string ToString(DateOnly value) => value.ToString("dd");
}