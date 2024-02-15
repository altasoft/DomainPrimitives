﻿//HintName: DecimalValue.g.cs
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

[JsonConverter(typeof(DecimalValueJsonConverter))]
[TypeConverter(typeof(DecimalValueTypeConverter))]
[DebuggerDisplay("{_value}")]
public readonly partial struct DecimalValue : IEquatable<DecimalValue>
        , IComparable
        , IComparable<DecimalValue>
        , IAdditionOperators<DecimalValue, DecimalValue, DecimalValue>
        , ISubtractionOperators<DecimalValue, DecimalValue, DecimalValue>
        , IMultiplyOperators<DecimalValue, DecimalValue, DecimalValue>
        , IDivisionOperators<DecimalValue, DecimalValue, DecimalValue>
        , IModulusOperators<DecimalValue, DecimalValue, DecimalValue>
        , IComparisonOperators<DecimalValue, DecimalValue, bool>
        , IParsable<DecimalValue>
        , IConvertible
#if NET8_0_OR_GREATER
        , IUtf8SpanFormattable
#endif
{
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly decimal _value;

    /// <summary>
    /// Initializes a new instance of the <see cref="DecimalValue"/> class by validating the specified <see cref="decimal"/> value using <see cref="Validate"/> static method.
    /// </summary>
    /// <param name="value">The value to be validated.</param>
    public DecimalValue(decimal value)
    {
        Validate(value);
        _value = value;
    }

    /// <inheritdoc/>
    [Obsolete("Domain primitive cannot be created using empty Ctor", true)]
    public DecimalValue()
    {
        _value = Default;
    }

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool Equals(object? obj) => obj is DecimalValue other && Equals(other);
    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(DecimalValue other) => _value == other._value;
    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(DecimalValue left, DecimalValue right) => left.Equals(right);
    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(DecimalValue left, DecimalValue right) => !(left == right);

    /// <inheritdoc/>
    public int CompareTo(object? obj)
    {
        if (obj is null)
            return 1;

        if (obj is DecimalValue c)
            return CompareTo(c);

        throw new ArgumentException("Object is not a DecimalValue", nameof(obj));
    }

    /// <inheritdoc/>
    public int CompareTo(DecimalValue other) => _value.CompareTo(other._value);

    /// <summary>
    /// Implicit conversion from <see cref = "decimal"/> to <see cref = "DecimalValue"/>
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator DecimalValue(decimal value) => new(value);

    /// <summary>
    /// Implicit conversion from <see cref = "decimal"/> (nullable) to <see cref = "DecimalValue"/> (nullable)
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [return: NotNullIfNotNull(nameof(value))]
    public static implicit operator DecimalValue?(decimal? value) => value is null ? null : new(value.Value);

    /// <summary>
    /// Implicit conversion from <see cref = "DecimalValue"/> to <see cref = "decimal"/>
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator decimal(DecimalValue value) => (decimal)value._value;

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DecimalValue operator +(DecimalValue left, DecimalValue right) => new(left._value + right._value);

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DecimalValue operator -(DecimalValue left, DecimalValue right) => new(left._value - right._value);

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DecimalValue operator *(DecimalValue left, DecimalValue right) => new(left._value * right._value);

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DecimalValue operator /(DecimalValue left, DecimalValue right) => new(left._value / right._value);

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DecimalValue operator %(DecimalValue left, DecimalValue right) => new(left._value % right._value);

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <(DecimalValue left, DecimalValue right) => left._value < right._value;

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <=(DecimalValue left, DecimalValue right) => left._value <= right._value;

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >(DecimalValue left, DecimalValue right) => left._value > right._value;

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >=(DecimalValue left, DecimalValue right) => left._value >= right._value;


    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DecimalValue Parse(string s, IFormatProvider? provider) => decimal.Parse(s, provider);

    /// <inheritdoc/>
    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out DecimalValue result)
    {
        if (!decimal.TryParse(s, provider, out var value))
        {
            result = default;
            return false;
        }

        try
        {
            result = new DecimalValue(value);
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
    TypeCode IConvertible.GetTypeCode() => ((IConvertible)(Decimal)_value).GetTypeCode();

    /// <inheritdoc/>
    bool IConvertible.ToBoolean(IFormatProvider? provider) => ((IConvertible)(Decimal)_value).ToBoolean(provider);

    /// <inheritdoc/>
    byte IConvertible.ToByte(IFormatProvider? provider) => ((IConvertible)(Decimal)_value).ToByte(provider);

    /// <inheritdoc/>
    char IConvertible.ToChar(IFormatProvider? provider) => ((IConvertible)(Decimal)_value).ToChar(provider);

    /// <inheritdoc/>
    DateTime IConvertible.ToDateTime(IFormatProvider? provider) => ((IConvertible)(Decimal)_value).ToDateTime(provider);

    /// <inheritdoc/>
    decimal IConvertible.ToDecimal(IFormatProvider? provider) => ((IConvertible)(Decimal)_value).ToDecimal(provider);

    /// <inheritdoc/>
    double IConvertible.ToDouble(IFormatProvider? provider) => ((IConvertible)(Decimal)_value).ToDouble(provider);

    /// <inheritdoc/>
    short IConvertible.ToInt16(IFormatProvider? provider) => ((IConvertible)(Decimal)_value).ToInt16(provider);

    /// <inheritdoc/>
    int IConvertible.ToInt32(IFormatProvider? provider) => ((IConvertible)(Decimal)_value).ToInt32(provider);

    /// <inheritdoc/>
    long IConvertible.ToInt64(IFormatProvider? provider) => ((IConvertible)(Decimal)_value).ToInt64(provider);

    /// <inheritdoc/>
    sbyte IConvertible.ToSByte(IFormatProvider? provider) => ((IConvertible)(Decimal)_value).ToSByte(provider);

    /// <inheritdoc/>
    float IConvertible.ToSingle(IFormatProvider? provider) => ((IConvertible)(Decimal)_value).ToSingle(provider);

    /// <inheritdoc/>
    string IConvertible.ToString(IFormatProvider? provider) => ((IConvertible)(Decimal)_value).ToString(provider);

    /// <inheritdoc/>
    object IConvertible.ToType(Type conversionType, IFormatProvider? provider) => ((IConvertible)(Decimal)_value).ToType(conversionType, provider);

    /// <inheritdoc/>
    ushort IConvertible.ToUInt16(IFormatProvider? provider) => ((IConvertible)(Decimal)_value).ToUInt16(provider);

    /// <inheritdoc/>
    uint IConvertible.ToUInt32(IFormatProvider? provider) => ((IConvertible)(Decimal)_value).ToUInt32(provider);

    /// <inheritdoc/>
    ulong IConvertible.ToUInt64(IFormatProvider? provider) => ((IConvertible)(Decimal)_value).ToUInt64(provider);

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString() => _value.ToString();
}
