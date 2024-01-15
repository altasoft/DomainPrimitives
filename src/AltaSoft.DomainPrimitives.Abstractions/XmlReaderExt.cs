using System;
using System.Runtime.CompilerServices;
using System.Xml;

// ReSharper disable UnusedMember.Global

namespace AltaSoft.DomainPrimitives.Abstractions;

/// <summary>
/// Provides extension methods for XmlReader and XmlWriter to simplify reading and writing of certain data types.
/// </summary>
public static class XmlReaderExt
{
	/// <summary>
	/// Reads the content of the current element as a <see cref="bool" /> object.
	/// </summary>
	/// <param name="reader">The XmlReader instance.</param>
	/// <returns>A bool object representing the value read from the element.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool ReadElementContentAsBool(this XmlReader reader) => reader.ReadElementContentAsBoolean();

	/// <summary>
	/// Reads the content of the current element as a <see cref="DateOnly" /> object.
	/// </summary>
	/// <param name="reader">The XmlReader instance.</param>
	/// <returns>A DateOnly object representing the date read from the element.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static DateOnly ReadElementContentAsDateOnly(this XmlReader reader) => DateOnly.FromDateTime(reader.ReadElementContentAsDateTime());

	/// <summary>
	/// Reads the content of the current element as a <see cref="TimeOnly" /> object.
	/// </summary>
	/// <param name="reader">The XmlReader instance.</param>
	/// <returns>A TimeOnly object representing the time read from the element.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static TimeOnly ReadElementContentAsTimeOnly(this XmlReader reader) => TimeOnly.FromDateTime(reader.ReadElementContentAsDateTime());
}