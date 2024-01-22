﻿//HintName: ByteValue.g.cs
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a AltaSoft.DomainPrimitives.Generator v1.0.0
//     Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

#nullable enable

using System;
using System.Numerics;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using AltaSoft.DomainPrimitives.Converters;
using System.ComponentModel;

namespace AltaSoft.DomainPrimitives;

[JsonConverter(typeof(ByteValueJsonConverter))]
[TypeConverter(typeof(ByteValueTypeConverter))]
[DebuggerDisplay("{_value}")]
public readonly partial struct ByteValue :
		IEquatable<ByteValue>
		, IComparable
		, IComparable<ByteValue>
		, IComparisonOperators<ByteValue, ByteValue, bool>
		, IParsable<ByteValue>
		, IConvertible
#if NET8_0_OR_GREATER
		, IUtf8SpanFormattable
#endif

{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private readonly byte _value;
	
	/// <summary>
	/// Initializes a new instance of the <see cref="ByteValue"/> class by validating the specified <see cref="byte"/> value using <see cref="Validate"/> static method.
	/// </summary>
	/// <param name="value">The value to be validated..</param>
	public ByteValue(byte value)
	{
			Validate(value);
			_value = value;
	}
	
	/// <inheritdoc/>
	[Obsolete("Domain primitive cannot be created using empty Ctor", true)]
	public ByteValue()
	{
			_value = Default;
	}
	
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public override bool Equals(object? obj) => obj is ByteValue other && Equals(other);
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool Equals(ByteValue other) => _value == other._value;
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool operator ==(ByteValue left, ByteValue right) => left.Equals(right);
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool operator !=(ByteValue left, ByteValue right) => !(left == right);

	/// <inheritdoc/>
	public int CompareTo(object? value)
	{
		if (value is null)
			return 1;

		if (value is ByteValue c)
			return CompareTo(c);

		throw new ArgumentException("Object is not a ByteValue", nameof(value));
	}

	/// <inheritdoc/>
	public int CompareTo(ByteValue other) => _value.CompareTo(other._value);

	/// <summary>
	/// Implicit conversion from <see cref = "byte"/> to <see cref = "ByteValue"/>
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static implicit operator ByteValue(byte value) => new(value);

	/// <summary>
	/// Implicit conversion from <see cref = "byte"/> (nullable) to <see cref = "ByteValue"/> (nullable)
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	[return: NotNullIfNotNull(nameof(value))]
	public static implicit operator ByteValue?(byte? value) => value is null ? null : new(value.Value);

	/// <summary>
	/// Implicit conversion from <see cref = "ByteValue"/> to <see cref = "byte"/>
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static implicit operator byte(ByteValue value) => (byte)value._value;

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool operator <(ByteValue left, ByteValue right) => left._value < right._value;

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool operator <=(ByteValue left, ByteValue right) => left._value <= right._value;

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool operator >(ByteValue left, ByteValue right) => left._value > right._value;

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool operator >=(ByteValue left, ByteValue right) => left._value >= right._value;


	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static ByteValue Parse(string s, IFormatProvider? provider) => byte.Parse(s, provider);

	/// <inheritdoc/>
	public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out ByteValue result)
	{
		if (!byte.TryParse(s, provider, out var value))
		{
			result = default;
			return false;
		}

		try
		{
			result = new ByteValue(value);
			return true;
		}
		catch (Exception)
		{
			result = default;
			return false;
		}
	}


#if NET8_0_OR_GREATER
	/// <inheritdoc cref="IUtf8SpanFormattable.TryFormat"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool TryFormat(Span<byte> utf8Destination, out int bytesWritten, ReadOnlySpan<char> format, IFormatProvider? provider)
	{
		return ((IUtf8SpanFormattable)_value).TryFormat(utf8Destination, out bytesWritten, format, provider);
	}
#endif

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public override int GetHashCode() => _value.GetHashCode();

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	TypeCode IConvertible.GetTypeCode() => ((IConvertible)(Byte)_value).GetTypeCode();

	/// <inheritdoc/>
	bool IConvertible.ToBoolean(IFormatProvider? provider) => ((IConvertible)(Byte)_value).ToBoolean(provider);

	/// <inheritdoc/>
	byte IConvertible.ToByte(IFormatProvider? provider) => ((IConvertible)(Byte)_value).ToByte(provider);

	/// <inheritdoc/>
	char IConvertible.ToChar(IFormatProvider? provider) => ((IConvertible)(Byte)_value).ToChar(provider);

	/// <inheritdoc/>
	DateTime IConvertible.ToDateTime(IFormatProvider? provider) => ((IConvertible)(Byte)_value).ToDateTime(provider);

	/// <inheritdoc/>
	decimal IConvertible.ToDecimal(IFormatProvider? provider) => ((IConvertible)(Byte)_value).ToDecimal(provider);

	/// <inheritdoc/>
	double IConvertible.ToDouble(IFormatProvider? provider) => ((IConvertible)(Byte)_value).ToDouble(provider);

	/// <inheritdoc/>
	short IConvertible.ToInt16(IFormatProvider? provider) => ((IConvertible)(Byte)_value).ToInt16(provider);

	/// <inheritdoc/>
	int IConvertible.ToInt32(IFormatProvider? provider) => ((IConvertible)(Byte)_value).ToInt32(provider);

	/// <inheritdoc/>
	long IConvertible.ToInt64(IFormatProvider? provider) => ((IConvertible)(Byte)_value).ToInt64(provider);

	/// <inheritdoc/>
	sbyte IConvertible.ToSByte(IFormatProvider? provider) => ((IConvertible)(Byte)_value).ToSByte(provider);

	/// <inheritdoc/>
	float IConvertible.ToSingle(IFormatProvider? provider) => ((IConvertible)(Byte)_value).ToSingle(provider);

	/// <inheritdoc/>
	string IConvertible.ToString(IFormatProvider? provider) => ((IConvertible)(Byte)_value).ToString(provider);

	/// <inheritdoc/>
	object IConvertible.ToType(Type conversionType, IFormatProvider? provider) => ((IConvertible)(Byte)_value).ToType(conversionType, provider);

	/// <inheritdoc/>
	ushort IConvertible.ToUInt16(IFormatProvider? provider) => ((IConvertible)(Byte)_value).ToUInt16(provider);

	/// <inheritdoc/>
	uint IConvertible.ToUInt32(IFormatProvider? provider) => ((IConvertible)(Byte)_value).ToUInt32(provider);

	/// <inheritdoc/>
	ulong IConvertible.ToUInt64(IFormatProvider? provider) => ((IConvertible)(Byte)_value).ToUInt64(provider);

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public override string ToString() => _value.ToString();

}
