﻿//HintName: ShortValue.g.cs
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
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using AltaSoft.DomainPrimitives.Converters;
using System.ComponentModel;

namespace AltaSoft.DomainPrimitives;

[JsonConverter(typeof(ShortValueJsonConverter))]
[TypeConverter(typeof(ShortValueTypeConverter))]
[DebuggerDisplay("{_value}")]
public readonly partial struct ShortValue :
		IEquatable<ShortValue>
		, IComparable
		, IComparable<ShortValue>
		, IComparisonOperators<ShortValue, ShortValue, bool>
		, IParsable<ShortValue>
		, IConvertible
#if NET8_0_OR_GREATER
		, IUtf8SpanFormattable
#endif

{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private readonly short _value;
	
	/// <summary>
	/// Initializes a new instance of the <see cref="ShortValue"/> class by validating the specified <see cref="short"/> value using <see cref="Validate"/> static method.
	/// </summary>
	/// <param name="value">The value to be validated..</param>
	public ShortValue(short value)
	{
			Validate(value);
			_value = value;
	}
	
	/// <inheritdoc/>
	[Obsolete("Domain primitive cannot be created using empty Ctor", true)]
	public ShortValue() : this(Default)
	{
	}
	
	/// <inheritdoc/>
	public override bool Equals(object? obj) => obj is ShortValue other && Equals(other);
	/// <inheritdoc/>
	public bool Equals(ShortValue other) => _value == other._value;
	/// <inheritdoc/>
	public static bool operator ==(ShortValue left, ShortValue right) => left.Equals(right);
	/// <inheritdoc/>
	public static bool operator !=(ShortValue left, ShortValue right) => !(left == right);

	/// <inheritdoc/>
	public int CompareTo(object? value)
	{
		if (value is null)
			return 1;

		if (value is ShortValue c)
			return CompareTo(c);

		throw new ArgumentException("Object is not a ShortValue", nameof(value));
	}

	/// <inheritdoc/>
	public int CompareTo(ShortValue other) => _value.CompareTo(other._value);

	/// <summary>
	/// Implicit conversion from <see cref = "short"/> to <see cref = "ShortValue"/>
	/// </summary>
	public static implicit operator ShortValue(short value) => new(value);

	/// <summary>
	/// Implicit conversion from <see cref = "short"/> (nullable) to <see cref = "ShortValue"/> (nullable)
	/// </summary>
	[return: NotNullIfNotNull(nameof(value))]
	public static implicit operator ShortValue?(short? value) => value is null ? null : new(value.Value);

	/// <summary>
	/// Implicit conversion from <see cref = "ShortValue"/> to <see cref = "short"/>
	/// </summary>
	public static implicit operator short(ShortValue value) => (short)value._value;

	/// <inheritdoc/>
	public static bool operator <(ShortValue left, ShortValue right) => left._value < right._value;

	/// <inheritdoc/>
	public static bool operator <=(ShortValue left, ShortValue right) => left._value <= right._value;

	/// <inheritdoc/>
	public static bool operator >(ShortValue left, ShortValue right) => left._value > right._value;

	/// <inheritdoc/>
	public static bool operator >=(ShortValue left, ShortValue right) => left._value >= right._value;


	/// <inheritdoc/>
	public static ShortValue Parse(string s, IFormatProvider? provider) => short.Parse(s, provider);

	/// <inheritdoc/>
	public static bool TryParse(string? s, IFormatProvider? provider, out ShortValue result)
	{
		if (!short.TryParse(s, provider, out var value))
		{
			result = default;
			return false;
		}

		try
		{
			result = new ShortValue(value);
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
	public bool TryFormat(Span<byte> utf8Destination, out int bytesWritten, ReadOnlySpan<char> format, IFormatProvider? provider)
	{
		return ((IUtf8SpanFormattable)_value).TryFormat(utf8Destination, out bytesWritten, format, provider);
	}
#endif

	/// <inheritdoc/>
	public override int GetHashCode() => _value.GetHashCode();
	/// <inheritdoc/>
	TypeCode IConvertible.GetTypeCode() => ((IConvertible)_value).GetTypeCode();

	/// <inheritdoc/>
	bool IConvertible.ToBoolean(IFormatProvider? provider) => ((IConvertible)_value).ToBoolean(provider);

	/// <inheritdoc/>
	byte IConvertible.ToByte(IFormatProvider? provider) => ((IConvertible)_value).ToByte(provider);

	/// <inheritdoc/>
	char IConvertible.ToChar(IFormatProvider? provider) => ((IConvertible)_value).ToChar(provider);

	/// <inheritdoc/>
	DateTime IConvertible.ToDateTime(IFormatProvider? provider) => ((IConvertible)_value).ToDateTime(provider);

	/// <inheritdoc/>
	decimal IConvertible.ToDecimal(IFormatProvider? provider) => ((IConvertible)_value).ToDecimal(provider);

	/// <inheritdoc/>
	double IConvertible.ToDouble(IFormatProvider? provider) => ((IConvertible)_value).ToDouble(provider);

	/// <inheritdoc/>
	short IConvertible.ToInt16(IFormatProvider? provider) => ((IConvertible)_value).ToInt16(provider);

	/// <inheritdoc/>
	int IConvertible.ToInt32(IFormatProvider? provider) => ((IConvertible)_value).ToInt32(provider);

	/// <inheritdoc/>
	long IConvertible.ToInt64(IFormatProvider? provider) => ((IConvertible)_value).ToInt64(provider);

	/// <inheritdoc/>
	sbyte IConvertible.ToSByte(IFormatProvider? provider) => ((IConvertible)_value).ToSByte(provider);

	/// <inheritdoc/>
	float IConvertible.ToSingle(IFormatProvider? provider) => ((IConvertible)_value).ToSingle(provider);

	/// <inheritdoc/>
	string IConvertible.ToString(IFormatProvider? provider) => ((IConvertible)_value).ToString(provider);

	/// <inheritdoc/>
	object IConvertible.ToType(Type conversionType, IFormatProvider? provider) => ((IConvertible)_value).ToType(conversionType, provider);

	/// <inheritdoc/>
	ushort IConvertible.ToUInt16(IFormatProvider? provider) => ((IConvertible)_value).ToUInt16(provider);

	/// <inheritdoc/>
	uint IConvertible.ToUInt32(IFormatProvider? provider) => ((IConvertible)_value).ToUInt32(provider);

	/// <inheritdoc/>
	ulong IConvertible.ToUInt64(IFormatProvider? provider) => ((IConvertible)_value).ToUInt64(provider);

	/// <inheritdoc/>
	public override string ToString() => _value.ToString();

}
