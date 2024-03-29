﻿//HintName: IntOfIntValue.g.cs
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
using AltaSoft.DomainPrimitives;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using AltaSoft.DomainPrimitives.Converters;
using System.ComponentModel;

namespace AltaSoft.DomainPrimitives;

[JsonConverter(typeof(IntOfIntValueJsonConverter))]
[TypeConverter(typeof(IntOfIntValueTypeConverter))]
[DebuggerDisplay("{_value}")]
public readonly partial struct IntOfIntValue : IEquatable<IntOfIntValue>
        , IComparable
        , IComparable<IntOfIntValue>
        , IAdditionOperators<IntOfIntValue, IntOfIntValue, IntOfIntValue>
        , ISubtractionOperators<IntOfIntValue, IntOfIntValue, IntOfIntValue>
        , IMultiplyOperators<IntOfIntValue, IntOfIntValue, IntOfIntValue>
        , IDivisionOperators<IntOfIntValue, IntOfIntValue, IntOfIntValue>
        , IModulusOperators<IntOfIntValue, IntOfIntValue, IntOfIntValue>
        , IComparisonOperators<IntOfIntValue, IntOfIntValue, bool>
        , IParsable<IntOfIntValue>
        , IConvertible
{
    /// <inheritdoc/>
     public Type GetUnderlyingPrimitiveType() => typeof(int);
    /// <inheritdoc/>
     public object GetUnderlyingPrimitiveValue() => (int)this;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly IntValue _value;

    /// <summary>
    /// Initializes a new instance of the <see cref="IntOfIntValue"/> class by validating the specified <see cref="IntValue"/> value using <see cref="Validate"/> static method.
    /// </summary>
    /// <param name="value">The value to be validated.</param>
    public IntOfIntValue(IntValue value)
    {
        Validate(value);
        _value = value;
    }

    /// <inheritdoc/>
    [Obsolete("Domain primitive cannot be created using empty Ctor", true)]
    public IntOfIntValue()
    {
        _value = Default;
    }

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool Equals(object? obj) => obj is IntOfIntValue other && Equals(other);
    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(IntOfIntValue other) => _value == other._value;
    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(IntOfIntValue left, IntOfIntValue right) => left.Equals(right);
    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(IntOfIntValue left, IntOfIntValue right) => !(left == right);

    /// <inheritdoc/>
    public int CompareTo(object? obj)
    {
        if (obj is null)
            return 1;

        if (obj is IntOfIntValue c)
            return CompareTo(c);

        throw new ArgumentException("Object is not a IntOfIntValue", nameof(obj));
    }

    /// <inheritdoc/>
    public int CompareTo(IntOfIntValue other) => _value.CompareTo(other._value);

    /// <summary>
    /// Implicit conversion from <see cref = "int"/> to <see cref = "IntOfIntValue"/>
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator IntOfIntValue(int value) => new(value);

    /// <summary>
    /// Implicit conversion from <see cref = "int"/> (nullable) to <see cref = "IntOfIntValue"/> (nullable)
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [return: NotNullIfNotNull(nameof(value))]
    public static implicit operator IntOfIntValue?(int? value) => value is null ? null : new(value.Value);

    /// <summary>
    /// Implicit conversion from <see cref = "IntOfIntValue"/> to <see cref = "int"/>
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator int(IntOfIntValue value) => (int)value._value;

    /// <summary>
    /// Implicit conversion from <see cref = "IntOfIntValue"/> (nullable) to <see cref = "int"/> (nullable)
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [return: NotNullIfNotNull(nameof(value))]
    public static implicit operator int?(IntOfIntValue? value) => value is null ? null : (int?)value.Value._value;

    /// <summary>
    /// Implicit conversion from <see cref = "IntValue"/> to <see cref = "IntOfIntValue"/>
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator IntOfIntValue(IntValue value) => new(value);

    /// <summary>
    /// Implicit conversion from <see cref = "IntValue"/> (nullable) to <see cref = "IntOfIntValue"/> (nullable)
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [return: NotNullIfNotNull(nameof(value))]
    public static implicit operator IntOfIntValue?(IntValue? value) => value is null ? null : (IntOfIntValue?)value.Value;

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IntOfIntValue operator +(IntOfIntValue left, IntOfIntValue right) => new(left._value + right._value);

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IntOfIntValue operator -(IntOfIntValue left, IntOfIntValue right) => new(left._value - right._value);

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IntOfIntValue operator *(IntOfIntValue left, IntOfIntValue right) => new(left._value * right._value);

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IntOfIntValue operator /(IntOfIntValue left, IntOfIntValue right) => new(left._value / right._value);

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IntOfIntValue operator %(IntOfIntValue left, IntOfIntValue right) => new(left._value % right._value);

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <(IntOfIntValue left, IntOfIntValue right) => left._value < right._value;

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <=(IntOfIntValue left, IntOfIntValue right) => left._value <= right._value;

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >(IntOfIntValue left, IntOfIntValue right) => left._value > right._value;

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >=(IntOfIntValue left, IntOfIntValue right) => left._value >= right._value;


    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IntOfIntValue Parse(string s, IFormatProvider? provider) => IntValue.Parse(s, provider);

    /// <inheritdoc/>
    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out IntOfIntValue result)
    {
        if (!IntValue.TryParse(s, provider, out var value))
        {
            result = default;
            return false;
        }

        try
        {
            result = new IntOfIntValue(value);
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
    TypeCode IConvertible.GetTypeCode() => ((IConvertible)(Int32)_value).GetTypeCode();

    /// <inheritdoc/>
    bool IConvertible.ToBoolean(IFormatProvider? provider) => ((IConvertible)(Int32)_value).ToBoolean(provider);

    /// <inheritdoc/>
    byte IConvertible.ToByte(IFormatProvider? provider) => ((IConvertible)(Int32)_value).ToByte(provider);

    /// <inheritdoc/>
    char IConvertible.ToChar(IFormatProvider? provider) => ((IConvertible)(Int32)_value).ToChar(provider);

    /// <inheritdoc/>
    DateTime IConvertible.ToDateTime(IFormatProvider? provider) => ((IConvertible)(Int32)_value).ToDateTime(provider);

    /// <inheritdoc/>
    decimal IConvertible.ToDecimal(IFormatProvider? provider) => ((IConvertible)(Int32)_value).ToDecimal(provider);

    /// <inheritdoc/>
    double IConvertible.ToDouble(IFormatProvider? provider) => ((IConvertible)(Int32)_value).ToDouble(provider);

    /// <inheritdoc/>
    short IConvertible.ToInt16(IFormatProvider? provider) => ((IConvertible)(Int32)_value).ToInt16(provider);

    /// <inheritdoc/>
    int IConvertible.ToInt32(IFormatProvider? provider) => ((IConvertible)(Int32)_value).ToInt32(provider);

    /// <inheritdoc/>
    long IConvertible.ToInt64(IFormatProvider? provider) => ((IConvertible)(Int32)_value).ToInt64(provider);

    /// <inheritdoc/>
    sbyte IConvertible.ToSByte(IFormatProvider? provider) => ((IConvertible)(Int32)_value).ToSByte(provider);

    /// <inheritdoc/>
    float IConvertible.ToSingle(IFormatProvider? provider) => ((IConvertible)(Int32)_value).ToSingle(provider);

    /// <inheritdoc/>
    string IConvertible.ToString(IFormatProvider? provider) => ((IConvertible)(Int32)_value).ToString(provider);

    /// <inheritdoc/>
    object IConvertible.ToType(Type conversionType, IFormatProvider? provider) => ((IConvertible)(Int32)_value).ToType(conversionType, provider);

    /// <inheritdoc/>
    ushort IConvertible.ToUInt16(IFormatProvider? provider) => ((IConvertible)(Int32)_value).ToUInt16(provider);

    /// <inheritdoc/>
    uint IConvertible.ToUInt32(IFormatProvider? provider) => ((IConvertible)(Int32)_value).ToUInt32(provider);

    /// <inheritdoc/>
    ulong IConvertible.ToUInt64(IFormatProvider? provider) => ((IConvertible)(Int32)_value).ToUInt64(provider);

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString() => _value.ToString();
}
