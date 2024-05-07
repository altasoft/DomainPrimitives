﻿//HintName: ShortValue.g.cs
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

[JsonConverter(typeof(ShortValueJsonConverter))]
[TypeConverter(typeof(ShortValueTypeConverter))]
[UnderlyingPrimitiveType(typeof(short))]
[DebuggerDisplay("{_value}")]
public readonly partial struct ShortValue : IEquatable<ShortValue>
        , IComparable
        , IComparable<ShortValue>
        , IComparisonOperators<ShortValue, ShortValue, bool>
        , IParsable<ShortValue>
        , IConvertible
#if NET8_0_OR_GREATER
        , IUtf8SpanFormattable
#endif
{
    /// <inheritdoc/>
     public Type GetUnderlyingPrimitiveType() => typeof(short);
    /// <inheritdoc/>
     public object GetUnderlyingPrimitiveValue() => (short)this;

    private short _valueOrThrow => _isInitialized ? _value : throw new InvalidDomainValueException("The domain value has not been initialized");
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly short _value;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly bool _isInitialized;

    /// <summary>
    /// Initializes a new instance of the <see cref="ShortValue"/> class by validating the specified <see cref="short"/> value using <see cref="Validate"/> static method.
    /// </summary>
    /// <param name="value">The value to be validated.</param>
    public ShortValue(short value)
    {
        Validate(value);
        _value = value;
        _isInitialized = true;
    }

    /// <inheritdoc/>
    [Obsolete("Domain primitive cannot be created using empty Ctor", true)]
    public ShortValue()
    {
    }

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool Equals(object? obj) => obj is ShortValue other && Equals(other);
    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(ShortValue other)
    {
        if (!_isInitialized || !other._isInitialized)
            return false;
        return _value.Equals(other._value);
    }
    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(ShortValue left, ShortValue right) => left.Equals(right);
    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(ShortValue left, ShortValue right) => !(left == right);

    /// <inheritdoc/>
    public int CompareTo(object? obj)
    {
        if (obj is null)
            return 1;

        if (obj is ShortValue c)
            return CompareTo(c);

        throw new ArgumentException("Object is not a ShortValue", nameof(obj));
    }

    /// <inheritdoc/>
    public int CompareTo(ShortValue other)
    {
        if (!other._isInitialized)
            return 1;
        if (!_isInitialized)
            return -1;
        return _value.CompareTo(other._value);
    }

    /// <summary>
    /// Implicit conversion from <see cref = "short"/> to <see cref = "ShortValue"/>
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator ShortValue(short value) => new(value);

    /// <summary>
    /// Implicit conversion from <see cref = "short"/> (nullable) to <see cref = "ShortValue"/> (nullable)
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [return: NotNullIfNotNull(nameof(value))]
    public static implicit operator ShortValue?(short? value) => value is null ? null : new(value.Value);

    /// <summary>
    /// Implicit conversion from <see cref = "ShortValue"/> to <see cref = "short"/>
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator short(ShortValue value) => (short)value._valueOrThrow;

    /// <summary>
    /// Implicit conversion from <see cref = "ShortValue"/> (nullable) to <see cref = "short"/> (nullable)
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [return: NotNullIfNotNull(nameof(value))]
    public static implicit operator short?(ShortValue? value) => value is null ? null : (short?)value.Value._valueOrThrow;

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <(ShortValue left, ShortValue right) => left._valueOrThrow < right._valueOrThrow;

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <=(ShortValue left, ShortValue right) => left._valueOrThrow <= right._valueOrThrow;

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >(ShortValue left, ShortValue right) => left._valueOrThrow > right._valueOrThrow;

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >=(ShortValue left, ShortValue right) => left._valueOrThrow >= right._valueOrThrow;


    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ShortValue Parse(string s, IFormatProvider? provider) => short.Parse(s, provider);

    /// <inheritdoc/>
    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out ShortValue result)
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
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryFormat(Span<byte> utf8Destination, out int bytesWritten, ReadOnlySpan<char> format, IFormatProvider? provider)
    {
        return ((IUtf8SpanFormattable)_valueOrThrow).TryFormat(utf8Destination, out bytesWritten, format, provider);
    }
#endif

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override int GetHashCode() => _valueOrThrow.GetHashCode();

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    TypeCode IConvertible.GetTypeCode() => ((IConvertible)(Int16)_valueOrThrow).GetTypeCode();

    /// <inheritdoc/>
    bool IConvertible.ToBoolean(IFormatProvider? provider) => ((IConvertible)(Int16)_valueOrThrow).ToBoolean(provider);

    /// <inheritdoc/>
    byte IConvertible.ToByte(IFormatProvider? provider) => ((IConvertible)(Int16)_valueOrThrow).ToByte(provider);

    /// <inheritdoc/>
    char IConvertible.ToChar(IFormatProvider? provider) => ((IConvertible)(Int16)_valueOrThrow).ToChar(provider);

    /// <inheritdoc/>
    DateTime IConvertible.ToDateTime(IFormatProvider? provider) => ((IConvertible)(Int16)_valueOrThrow).ToDateTime(provider);

    /// <inheritdoc/>
    decimal IConvertible.ToDecimal(IFormatProvider? provider) => ((IConvertible)(Int16)_valueOrThrow).ToDecimal(provider);

    /// <inheritdoc/>
    double IConvertible.ToDouble(IFormatProvider? provider) => ((IConvertible)(Int16)_valueOrThrow).ToDouble(provider);

    /// <inheritdoc/>
    short IConvertible.ToInt16(IFormatProvider? provider) => ((IConvertible)(Int16)_valueOrThrow).ToInt16(provider);

    /// <inheritdoc/>
    int IConvertible.ToInt32(IFormatProvider? provider) => ((IConvertible)(Int16)_valueOrThrow).ToInt32(provider);

    /// <inheritdoc/>
    long IConvertible.ToInt64(IFormatProvider? provider) => ((IConvertible)(Int16)_valueOrThrow).ToInt64(provider);

    /// <inheritdoc/>
    sbyte IConvertible.ToSByte(IFormatProvider? provider) => ((IConvertible)(Int16)_valueOrThrow).ToSByte(provider);

    /// <inheritdoc/>
    float IConvertible.ToSingle(IFormatProvider? provider) => ((IConvertible)(Int16)_valueOrThrow).ToSingle(provider);

    /// <inheritdoc/>
    string IConvertible.ToString(IFormatProvider? provider) => ((IConvertible)(Int16)_valueOrThrow).ToString(provider);

    /// <inheritdoc/>
    object IConvertible.ToType(Type conversionType, IFormatProvider? provider) => ((IConvertible)(Int16)_valueOrThrow).ToType(conversionType, provider);

    /// <inheritdoc/>
    ushort IConvertible.ToUInt16(IFormatProvider? provider) => ((IConvertible)(Int16)_valueOrThrow).ToUInt16(provider);

    /// <inheritdoc/>
    uint IConvertible.ToUInt32(IFormatProvider? provider) => ((IConvertible)(Int16)_valueOrThrow).ToUInt32(provider);

    /// <inheritdoc/>
    ulong IConvertible.ToUInt64(IFormatProvider? provider) => ((IConvertible)(Int16)_valueOrThrow).ToUInt64(provider);

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString() => _valueOrThrow.ToString();
}
