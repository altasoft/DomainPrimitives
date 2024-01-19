using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Xml;

// ReSharper disable UnusedMember.Global

namespace AltaSoft.DomainPrimitives;

/// <summary>
/// Provides extension methods for XmlReader and XmlWriter to simplify reading and writing of certain data types.
/// </summary>
public static class XmlReaderExt
{
	/// <summary>
	/// Reads the content of the current element as a <see cref="byte" /> object.
	/// </summary>
	/// <param name="reader">The XmlReader instance.</param>
	/// <returns>A byte object representing the value read from the element.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static T ReadElementContentAs<T>(this XmlReader reader) where T : IParsable<T>
		=> T.Parse(reader.ReadElementContentAsString(), CultureInfo.InvariantCulture);

	///// <summary>
	///// Reads the content of the current element as a <see cref="bool" /> object.
	///// </summary>
	///// <param name="reader">The XmlReader instance.</param>
	///// <returns>A bool object representing the value read from the element.</returns>
	//[MethodImpl(MethodImplOptions.AggressiveInlining)]
	//public static bool ReadElementContentAsBool(this XmlReader reader) => reader.ReadElementContentAsBoolean();

	///// <summary>
	///// Reads the content of the current element as a <see cref="byte" /> object.
	///// </summary>
	///// <param name="reader">The XmlReader instance.</param>
	///// <returns>A byte object representing the value read from the element.</returns>
	//[MethodImpl(MethodImplOptions.AggressiveInlining)]
	//public static byte ReadElementContentAsByte(this XmlReader reader) => byte.Parse(reader.ReadElementContentAsString(), CultureInfo.InvariantCulture);

	///// <summary>
	///// Reads the content of the current element as a <see cref="sbyte" /> object.
	///// </summary>
	///// <param name="reader">The XmlReader instance.</param>
	///// <returns>A sbyte object representing the value read from the element.</returns>
	//[MethodImpl(MethodImplOptions.AggressiveInlining)]
	//public static sbyte ReadElementContentAsSByte(this XmlReader reader) => (sbyte)reader.ReadElementContentAsInt();

	///// <summary>
	///// Reads the content of the current element as a <see cref="short" /> object.
	///// </summary>
	///// <param name="reader">The XmlReader instance.</param>
	///// <returns>A short object representing the value read from the element.</returns>
	//[MethodImpl(MethodImplOptions.AggressiveInlining)]
	//public static short ReadElementContentAsInt16(this XmlReader reader) => (short)reader.ReadElementContentAsInt();

	///// <summary>
	///// Reads the content of the current element as a <see cref="ushort" /> object.
	///// </summary>
	///// <param name="reader">The XmlReader instance.</param>
	///// <returns>A ushort object representing the value read from the element.</returns>
	//[MethodImpl(MethodImplOptions.AggressiveInlining)]
	//public static ushort ReadElementContentAsUInt16(this XmlReader reader) => (ushort)reader.ReadElementContentAsInt();

	///// <summary>
	///// Reads the content of the current element as a <see cref="int" /> object.
	///// </summary>
	///// <param name="reader">The XmlReader instance.</param>
	///// <returns>A int object representing the value read from the element.</returns>
	//[MethodImpl(MethodImplOptions.AggressiveInlining)]
	//public static int ReadElementContentAsInt32(this XmlReader reader) => reader.ReadElementContentAsInt();

	///// <summary>
	///// Reads the content of the current element as a <see cref="uint" /> object.
	///// </summary>
	///// <param name="reader">The XmlReader instance.</param>
	///// <returns>A uint object representing the value read from the element.</returns>
	//[MethodImpl(MethodImplOptions.AggressiveInlining)]
	//public static int ReadElementContentAsUInt32(this XmlReader reader) => reader.ReadElementContentAsInt();

	///// <summary>
	///// Reads the content of the current element as a <see cref="long" /> object.
	///// </summary>
	///// <param name="reader">The XmlReader instance.</param>
	///// <returns>A long object representing the value read from the element.</returns>
	//[MethodImpl(MethodImplOptions.AggressiveInlining)]
	//public static long ReadElementContentAsInt64(this XmlReader reader) => reader.ReadElementContentAsLong();

	///// <summary>
	///// Reads the content of the current element as a <see cref="ulong" /> object.
	///// </summary>
	///// <param name="reader">The XmlReader instance.</param>
	///// <returns>A ulong object representing the value read from the element.</returns>
	//[MethodImpl(MethodImplOptions.AggressiveInlining)]
	//public static ulong ReadElementContentAsUInt64(this XmlReader reader) => reader.ReadElementContentAsLong();

	///// <summary>
	///// Reads the content of the current element as a <see cref="DateOnly" /> object.
	///// </summary>
	///// <param name="reader">The XmlReader instance.</param>
	///// <returns>A DateOnly object representing the date read from the element.</returns>
	//[MethodImpl(MethodImplOptions.AggressiveInlining)]
	//public static DateOnly ReadElementContentAsDateOnly(this XmlReader reader) => DateOnly.FromDateTime(reader.ReadElementContentAsDateTime());

	///// <summary>
	///// Reads the content of the current element as a <see cref="TimeOnly" /> object.
	///// </summary>
	///// <param name="reader">The XmlReader instance.</param>
	///// <returns>A TimeOnly object representing the time read from the element.</returns>
	//[MethodImpl(MethodImplOptions.AggressiveInlining)]
	//public static TimeOnly ReadElementContentAsTimeOnly(this XmlReader reader) => TimeOnly.FromDateTime(reader.ReadElementContentAsDateTime());
}