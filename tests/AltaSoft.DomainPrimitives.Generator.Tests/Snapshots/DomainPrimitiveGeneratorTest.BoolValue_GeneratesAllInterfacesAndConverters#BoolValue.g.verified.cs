﻿//HintName: BoolValue.g.cs
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

[JsonConverter(typeof(BoolValueJsonConverter))]
[TypeConverter(typeof(BoolValueTypeConverter))]
[DebuggerDisplay("{_value}")]
public readonly partial struct BoolValue :
		IEquatable<BoolValue>
		, IComparable
		, IComparable<BoolValue>
		, IParsable<BoolValue>
		, IConvertible
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private readonly bool _value;
	
	/// <summary>
	/// Initializes a new instance of the <see cref="BoolValue"/> class by validating the specified <see cref="bool"/> value using <see cref="Validate"/> static method.
	/// </summary>
	/// <param name="value">The value to be validated..</param>
	public BoolValue(bool value)
	{
			Validate(value);
			_value = value;
	}
	
	/// <inheritdoc/>
	[Obsolete("Domain primitive cannot be created using empty Ctor", true)]
	public BoolValue()
	{
			_value = Default;
	}
	
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public override bool Equals(object? obj) => obj is BoolValue other && Equals(other);
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool Equals(BoolValue other) => _value == other._value;
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool operator ==(BoolValue left, BoolValue right) => left.Equals(right);
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool operator !=(BoolValue left, BoolValue right) => !(left == right);

	/// <inheritdoc/>
	public int CompareTo(object? value)
	{
		if (value is null)
			return 1;

		if (value is BoolValue c)
			return CompareTo(c);

		throw new ArgumentException("Object is not a BoolValue", nameof(value));
	}

	/// <inheritdoc/>
	public int CompareTo(BoolValue other) => _value.CompareTo(other._value);

	/// <summary>
	/// Implicit conversion from <see cref = "bool"/> to <see cref = "BoolValue"/>
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static implicit operator BoolValue(bool value) => new(value);

	/// <summary>
	/// Implicit conversion from <see cref = "bool"/> (nullable) to <see cref = "BoolValue"/> (nullable)
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	[return: NotNullIfNotNull(nameof(value))]
	public static implicit operator BoolValue?(bool? value) => value is null ? null : new(value.Value);

	/// <summary>
	/// Implicit conversion from <see cref = "BoolValue"/> to <see cref = "bool"/>
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static implicit operator bool(BoolValue value) => (bool)value._value;

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static BoolValue Parse(string s, IFormatProvider? provider) => bool.Parse(s);

	/// <inheritdoc/>
	public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out BoolValue result)
	{
		if (!bool.TryParse(s, out var value))
		{
			result = default;
			return false;
		}

		try
		{
			result = new BoolValue(value);
			return true;
		}
		catch (Exception)
		{
			result = default;
			return false;
		}
	}


	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public override int GetHashCode() => _value.GetHashCode();

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	TypeCode IConvertible.GetTypeCode() => ((IConvertible)(Boolean)_value).GetTypeCode();

	/// <inheritdoc/>
	bool IConvertible.ToBoolean(IFormatProvider? provider) => ((IConvertible)(Boolean)_value).ToBoolean(provider);

	/// <inheritdoc/>
	byte IConvertible.ToByte(IFormatProvider? provider) => ((IConvertible)(Boolean)_value).ToByte(provider);

	/// <inheritdoc/>
	char IConvertible.ToChar(IFormatProvider? provider) => ((IConvertible)(Boolean)_value).ToChar(provider);

	/// <inheritdoc/>
	DateTime IConvertible.ToDateTime(IFormatProvider? provider) => ((IConvertible)(Boolean)_value).ToDateTime(provider);

	/// <inheritdoc/>
	decimal IConvertible.ToDecimal(IFormatProvider? provider) => ((IConvertible)(Boolean)_value).ToDecimal(provider);

	/// <inheritdoc/>
	double IConvertible.ToDouble(IFormatProvider? provider) => ((IConvertible)(Boolean)_value).ToDouble(provider);

	/// <inheritdoc/>
	short IConvertible.ToInt16(IFormatProvider? provider) => ((IConvertible)(Boolean)_value).ToInt16(provider);

	/// <inheritdoc/>
	int IConvertible.ToInt32(IFormatProvider? provider) => ((IConvertible)(Boolean)_value).ToInt32(provider);

	/// <inheritdoc/>
	long IConvertible.ToInt64(IFormatProvider? provider) => ((IConvertible)(Boolean)_value).ToInt64(provider);

	/// <inheritdoc/>
	sbyte IConvertible.ToSByte(IFormatProvider? provider) => ((IConvertible)(Boolean)_value).ToSByte(provider);

	/// <inheritdoc/>
	float IConvertible.ToSingle(IFormatProvider? provider) => ((IConvertible)(Boolean)_value).ToSingle(provider);

	/// <inheritdoc/>
	string IConvertible.ToString(IFormatProvider? provider) => ((IConvertible)(Boolean)_value).ToString(provider);

	/// <inheritdoc/>
	object IConvertible.ToType(Type conversionType, IFormatProvider? provider) => ((IConvertible)(Boolean)_value).ToType(conversionType, provider);

	/// <inheritdoc/>
	ushort IConvertible.ToUInt16(IFormatProvider? provider) => ((IConvertible)(Boolean)_value).ToUInt16(provider);

	/// <inheritdoc/>
	uint IConvertible.ToUInt32(IFormatProvider? provider) => ((IConvertible)(Boolean)_value).ToUInt32(provider);

	/// <inheritdoc/>
	ulong IConvertible.ToUInt64(IFormatProvider? provider) => ((IConvertible)(Boolean)_value).ToUInt64(provider);

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public override string ToString() => _value.ToString();

}
