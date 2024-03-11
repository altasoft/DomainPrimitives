﻿//HintName: IntValue.g.cs
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by 'AltaSoft DomainPrimitives Generator'.
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

[JsonConverter(typeof(IntValueJsonConverter))]
[TypeConverter(typeof(IntValueTypeConverter))]
[DebuggerDisplay("{_value}")]
public readonly partial struct IntValue : IEquatable<IntValue>
        , IComparable
        , IComparable<IntValue>
        , IAdditionOperators<IntValue, IntValue, IntValue>
        , ISubtractionOperators<IntValue, IntValue, IntValue>
        , IMultiplyOperators<IntValue, IntValue, IntValue>
        , IDivisionOperators<IntValue, IntValue, IntValue>
        , IModulusOperators<IntValue, IntValue, IntValue>
        , IComparisonOperators<IntValue, IntValue, bool>
        , IParsable<IntValue>
        , IConvertible
#if NET8_0_OR_GREATER
        , IUtf8SpanFormattable
#endif
{
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly uint _value;

    /// <summary>
    /// Initializes a new instance of the <see cref="IntValue"/> class by validating the specified <see cref="uint"/> value using <see cref="Validate"/> static method.
    /// </summary>
    /// <param name="value">The value to be validated.</param>
    public IntValue(uint value)
    {
        Validate(value);
        _value = value;
    }

    /// <inheritdoc/>
    [Obsolete("Domain primitive cannot be created using empty Ctor", true)]
    public IntValue()
    {
        _value = Default;
    }

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool Equals(object? obj) => obj is IntValue other && Equals(other);
    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(IntValue other) => _value == other._value;
    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(IntValue left, IntValue right) => left.Equals(right);
    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(IntValue left, IntValue right) => !(left == right);

    /// <inheritdoc/>
    public int CompareTo(object? obj)
    {
        if (obj is null)
            return 1;

        if (obj is IntValue c)
            return CompareTo(c);

        throw new ArgumentException("Object is not a IntValue", nameof(obj));
    }

    /// <inheritdoc/>
    public int CompareTo(IntValue other) => _value.CompareTo(other._value);

    /// <summary>
    /// Implicit conversion from <see cref = "uint"/> to <see cref = "IntValue"/>
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator IntValue(uint value) => new(value);

    /// <summary>
    /// Implicit conversion from <see cref = "uint"/> (nullable) to <see cref = "IntValue"/> (nullable)
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [return: NotNullIfNotNull(nameof(value))]
    public static implicit operator IntValue?(uint? value) => value is null ? null : new(value.Value);

    /// <summary>
    /// Implicit conversion from <see cref = "IntValue"/> to <see cref = "uint"/>
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator uint(IntValue value) => (uint)value._value;

    /// <summary>
    /// Implicit conversion from <see cref = "IntValue"/> (nullable) to <see cref = "uint"/> (nullable)
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [return: NotNullIfNotNull(nameof(value))]
    public static implicit operator uint?(IntValue? value) => value is null ? null : (uint?)value.Value._value;

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IntValue operator +(IntValue left, IntValue right) => new(left._value + right._value);

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IntValue operator -(IntValue left, IntValue right) => new(left._value - right._value);

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IntValue operator *(IntValue left, IntValue right) => new(left._value * right._value);

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IntValue operator /(IntValue left, IntValue right) => new(left._value / right._value);

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IntValue operator %(IntValue left, IntValue right) => new(left._value % right._value);

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <(IntValue left, IntValue right) => left._value < right._value;

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <=(IntValue left, IntValue right) => left._value <= right._value;

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >(IntValue left, IntValue right) => left._value > right._value;

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >=(IntValue left, IntValue right) => left._value >= right._value;


    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IntValue Parse(string s, IFormatProvider? provider) => uint.Parse(s, provider);

    /// <inheritdoc/>
    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out IntValue result)
    {
        if (!uint.TryParse(s, provider, out var value))
        {
            result = default;
            return false;
        }

        try
        {
            result = new IntValue(value);
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
    TypeCode IConvertible.GetTypeCode() => ((IConvertible)(UInt32)_value).GetTypeCode();

    /// <inheritdoc/>
    bool IConvertible.ToBoolean(IFormatProvider? provider) => ((IConvertible)(UInt32)_value).ToBoolean(provider);

    /// <inheritdoc/>
    byte IConvertible.ToByte(IFormatProvider? provider) => ((IConvertible)(UInt32)_value).ToByte(provider);

    /// <inheritdoc/>
    char IConvertible.ToChar(IFormatProvider? provider) => ((IConvertible)(UInt32)_value).ToChar(provider);

    /// <inheritdoc/>
    DateTime IConvertible.ToDateTime(IFormatProvider? provider) => ((IConvertible)(UInt32)_value).ToDateTime(provider);

    /// <inheritdoc/>
    decimal IConvertible.ToDecimal(IFormatProvider? provider) => ((IConvertible)(UInt32)_value).ToDecimal(provider);

    /// <inheritdoc/>
    double IConvertible.ToDouble(IFormatProvider? provider) => ((IConvertible)(UInt32)_value).ToDouble(provider);

    /// <inheritdoc/>
    short IConvertible.ToInt16(IFormatProvider? provider) => ((IConvertible)(UInt32)_value).ToInt16(provider);

    /// <inheritdoc/>
    int IConvertible.ToInt32(IFormatProvider? provider) => ((IConvertible)(UInt32)_value).ToInt32(provider);

    /// <inheritdoc/>
    long IConvertible.ToInt64(IFormatProvider? provider) => ((IConvertible)(UInt32)_value).ToInt64(provider);

    /// <inheritdoc/>
    sbyte IConvertible.ToSByte(IFormatProvider? provider) => ((IConvertible)(UInt32)_value).ToSByte(provider);

    /// <inheritdoc/>
    float IConvertible.ToSingle(IFormatProvider? provider) => ((IConvertible)(UInt32)_value).ToSingle(provider);

    /// <inheritdoc/>
    string IConvertible.ToString(IFormatProvider? provider) => ((IConvertible)(UInt32)_value).ToString(provider);

    /// <inheritdoc/>
    object IConvertible.ToType(Type conversionType, IFormatProvider? provider) => ((IConvertible)(UInt32)_value).ToType(conversionType, provider);

    /// <inheritdoc/>
    ushort IConvertible.ToUInt16(IFormatProvider? provider) => ((IConvertible)(UInt32)_value).ToUInt16(provider);

    /// <inheritdoc/>
    uint IConvertible.ToUInt32(IFormatProvider? provider) => ((IConvertible)(UInt32)_value).ToUInt32(provider);

    /// <inheritdoc/>
    ulong IConvertible.ToUInt64(IFormatProvider? provider) => ((IConvertible)(UInt32)_value).ToUInt64(provider);

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString() => _value.ToString();
}
