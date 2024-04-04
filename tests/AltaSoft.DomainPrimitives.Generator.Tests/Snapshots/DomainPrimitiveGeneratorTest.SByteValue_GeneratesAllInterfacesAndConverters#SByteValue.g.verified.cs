﻿//HintName: SByteValue.g.cs
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

[JsonConverter(typeof(SByteValueJsonConverter))]
[TypeConverter(typeof(SByteValueTypeConverter))]
[UnderlyingPrimitiveType(typeof(sbyte))]
[DebuggerDisplay("{_value}")]
public readonly partial struct SByteValue : IEquatable<SByteValue>
        , IComparable
        , IComparable<SByteValue>
        , IComparisonOperators<SByteValue, SByteValue, bool>
        , IParsable<SByteValue>
        , IConvertible
#if NET8_0_OR_GREATER
        , IUtf8SpanFormattable
#endif
{
    /// <inheritdoc/>
     public Type GetUnderlyingPrimitiveType() => typeof(sbyte);
    /// <inheritdoc/>
     public object GetUnderlyingPrimitiveValue() => (sbyte)this;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly sbyte _value;

    /// <summary>
    /// Initializes a new instance of the <see cref="SByteValue"/> class by validating the specified <see cref="sbyte"/> value using <see cref="Validate"/> static method.
    /// </summary>
    /// <param name="value">The value to be validated.</param>
    public SByteValue(sbyte value)
    {
        Validate(value);
        _value = value;
    }

    /// <inheritdoc/>
    [Obsolete("Domain primitive cannot be created using empty Ctor", true)]
    public SByteValue()
    {
        _value = Default;
    }

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool Equals(object? obj) => obj is SByteValue other && Equals(other);
    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(SByteValue other) => _value == other._value;
    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(SByteValue left, SByteValue right) => left.Equals(right);
    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(SByteValue left, SByteValue right) => !(left == right);

    /// <inheritdoc/>
    public int CompareTo(object? obj)
    {
        if (obj is null)
            return 1;

        if (obj is SByteValue c)
            return CompareTo(c);

        throw new ArgumentException("Object is not a SByteValue", nameof(obj));
    }

    /// <inheritdoc/>
    public int CompareTo(SByteValue other) => _value.CompareTo(other._value);

    /// <summary>
    /// Implicit conversion from <see cref = "sbyte"/> to <see cref = "SByteValue"/>
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator SByteValue(sbyte value) => new(value);

    /// <summary>
    /// Implicit conversion from <see cref = "sbyte"/> (nullable) to <see cref = "SByteValue"/> (nullable)
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [return: NotNullIfNotNull(nameof(value))]
    public static implicit operator SByteValue?(sbyte? value) => value is null ? null : new(value.Value);

    /// <summary>
    /// Implicit conversion from <see cref = "SByteValue"/> to <see cref = "sbyte"/>
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator sbyte(SByteValue value) => (sbyte)value._value;

    /// <summary>
    /// Implicit conversion from <see cref = "SByteValue"/> (nullable) to <see cref = "sbyte"/> (nullable)
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [return: NotNullIfNotNull(nameof(value))]
    public static implicit operator sbyte?(SByteValue? value) => value is null ? null : (sbyte?)value.Value._value;

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <(SByteValue left, SByteValue right) => left._value < right._value;

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <=(SByteValue left, SByteValue right) => left._value <= right._value;

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >(SByteValue left, SByteValue right) => left._value > right._value;

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >=(SByteValue left, SByteValue right) => left._value >= right._value;


    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SByteValue Parse(string s, IFormatProvider? provider) => sbyte.Parse(s, provider);

    /// <inheritdoc/>
    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out SByteValue result)
    {
        if (!sbyte.TryParse(s, provider, out var value))
        {
            result = default;
            return false;
        }

        try
        {
            result = new SByteValue(value);
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
    TypeCode IConvertible.GetTypeCode() => ((IConvertible)(SByte)_value).GetTypeCode();

    /// <inheritdoc/>
    bool IConvertible.ToBoolean(IFormatProvider? provider) => ((IConvertible)(SByte)_value).ToBoolean(provider);

    /// <inheritdoc/>
    byte IConvertible.ToByte(IFormatProvider? provider) => ((IConvertible)(SByte)_value).ToByte(provider);

    /// <inheritdoc/>
    char IConvertible.ToChar(IFormatProvider? provider) => ((IConvertible)(SByte)_value).ToChar(provider);

    /// <inheritdoc/>
    DateTime IConvertible.ToDateTime(IFormatProvider? provider) => ((IConvertible)(SByte)_value).ToDateTime(provider);

    /// <inheritdoc/>
    decimal IConvertible.ToDecimal(IFormatProvider? provider) => ((IConvertible)(SByte)_value).ToDecimal(provider);

    /// <inheritdoc/>
    double IConvertible.ToDouble(IFormatProvider? provider) => ((IConvertible)(SByte)_value).ToDouble(provider);

    /// <inheritdoc/>
    short IConvertible.ToInt16(IFormatProvider? provider) => ((IConvertible)(SByte)_value).ToInt16(provider);

    /// <inheritdoc/>
    int IConvertible.ToInt32(IFormatProvider? provider) => ((IConvertible)(SByte)_value).ToInt32(provider);

    /// <inheritdoc/>
    long IConvertible.ToInt64(IFormatProvider? provider) => ((IConvertible)(SByte)_value).ToInt64(provider);

    /// <inheritdoc/>
    sbyte IConvertible.ToSByte(IFormatProvider? provider) => ((IConvertible)(SByte)_value).ToSByte(provider);

    /// <inheritdoc/>
    float IConvertible.ToSingle(IFormatProvider? provider) => ((IConvertible)(SByte)_value).ToSingle(provider);

    /// <inheritdoc/>
    string IConvertible.ToString(IFormatProvider? provider) => ((IConvertible)(SByte)_value).ToString(provider);

    /// <inheritdoc/>
    object IConvertible.ToType(Type conversionType, IFormatProvider? provider) => ((IConvertible)(SByte)_value).ToType(conversionType, provider);

    /// <inheritdoc/>
    ushort IConvertible.ToUInt16(IFormatProvider? provider) => ((IConvertible)(SByte)_value).ToUInt16(provider);

    /// <inheritdoc/>
    uint IConvertible.ToUInt32(IFormatProvider? provider) => ((IConvertible)(SByte)_value).ToUInt32(provider);

    /// <inheritdoc/>
    ulong IConvertible.ToUInt64(IFormatProvider? provider) => ((IConvertible)(SByte)_value).ToUInt64(provider);

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString() => _value.ToString();
}
