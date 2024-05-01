﻿//HintName: TimeOnlyValue.g.cs
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

[JsonConverter(typeof(TimeOnlyValueJsonConverter))]
[TypeConverter(typeof(TimeOnlyValueTypeConverter))]
[UnderlyingPrimitiveType(typeof(TimeOnly))]
[DebuggerDisplay("{_value}")]
public readonly partial struct TimeOnlyValue : IEquatable<TimeOnlyValue>
        , IComparable
        , IComparable<TimeOnlyValue>
        , IComparisonOperators<TimeOnlyValue, TimeOnlyValue, bool>
        , ISpanFormattable
        , IParsable<TimeOnlyValue>
        , IConvertible
#if NET8_0_OR_GREATER
        , IUtf8SpanFormattable
#endif
{
    /// <inheritdoc/>
     public Type GetUnderlyingPrimitiveType() => typeof(TimeOnly);
    /// <inheritdoc/>
     public object GetUnderlyingPrimitiveValue() => (TimeOnly)this;

    private TimeOnly _valueOrThrow => _isInitialized ? _value : throw new InvalidDomainValueException("The domain value has not been initialized");
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly TimeOnly _value;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly bool _isInitialized;

    /// <summary>
    /// Initializes a new instance of the <see cref="TimeOnlyValue"/> class by validating the specified <see cref="TimeOnly"/> value using <see cref="Validate"/> static method.
    /// </summary>
    /// <param name="value">The value to be validated.</param>
    public TimeOnlyValue(TimeOnly value)
    {
        Validate(value);
        _value = value;
        _isInitialized = true;
    }

    /// <inheritdoc/>
    [Obsolete("Domain primitive cannot be created using empty Ctor", true)]
    public TimeOnlyValue()
    {
    }

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool Equals(object? obj) => obj is TimeOnlyValue other && Equals(other);
    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(TimeOnlyValue other) => _valueOrThrow == other._valueOrThrow;
    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(TimeOnlyValue left, TimeOnlyValue right) => left.Equals(right);
    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(TimeOnlyValue left, TimeOnlyValue right) => !(left == right);

    /// <inheritdoc/>
    public int CompareTo(object? obj)
    {
        if (obj is null)
            return 1;

        if (obj is TimeOnlyValue c)
            return CompareTo(c);

        throw new ArgumentException("Object is not a TimeOnlyValue", nameof(obj));
    }

    /// <inheritdoc/>
    public int CompareTo(TimeOnlyValue other) => _valueOrThrow.CompareTo(other._valueOrThrow);

    /// <summary>
    /// Implicit conversion from <see cref = "TimeOnly"/> to <see cref = "TimeOnlyValue"/>
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator TimeOnlyValue(TimeOnly value) => new(value);

    /// <summary>
    /// Implicit conversion from <see cref = "TimeOnly"/> (nullable) to <see cref = "TimeOnlyValue"/> (nullable)
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [return: NotNullIfNotNull(nameof(value))]
    public static implicit operator TimeOnlyValue?(TimeOnly? value) => value is null ? null : new(value.Value);

    /// <summary>
    /// Implicit conversion from <see cref = "TimeOnlyValue"/> to <see cref = "TimeOnly"/>
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator TimeOnly(TimeOnlyValue value) => (TimeOnly)value._valueOrThrow;

    /// <summary>
    /// Implicit conversion from <see cref = "TimeOnlyValue"/> (nullable) to <see cref = "TimeOnly"/> (nullable)
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [return: NotNullIfNotNull(nameof(value))]
    public static implicit operator TimeOnly?(TimeOnlyValue? value) => value is null ? null : (TimeOnly?)value.Value._valueOrThrow;

    /// <summary>
    /// Implicit conversion from <see cref = "TimeOnlyValue"/> to <see cref = "DateTime"/>
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator DateTime(TimeOnlyValue value) => ((TimeOnly)value._valueOrThrow).ToDateTime();

    /// <summary>
    /// Implicit conversion from <see cref = "DateTime"/> to <see cref = "TimeOnlyValue"/>
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator TimeOnlyValue(DateTime value) => TimeOnly.FromDateTime(value);

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <(TimeOnlyValue left, TimeOnlyValue right) => left._valueOrThrow < right._valueOrThrow;

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <=(TimeOnlyValue left, TimeOnlyValue right) => left._valueOrThrow <= right._valueOrThrow;

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >(TimeOnlyValue left, TimeOnlyValue right) => left._valueOrThrow > right._valueOrThrow;

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >=(TimeOnlyValue left, TimeOnlyValue right) => left._valueOrThrow >= right._valueOrThrow;


    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TimeOnlyValue Parse(string s, IFormatProvider? provider) => TimeOnly.Parse(s, provider);

    /// <inheritdoc/>
    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out TimeOnlyValue result)
    {
        if (!TimeOnly.TryParse(s, provider, out var value))
        {
            result = default;
            return false;
        }

        try
        {
            result = new TimeOnlyValue(value);
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
    public string ToString(string? format, IFormatProvider? formatProvider) => _valueOrThrow.ToString(format, formatProvider);

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider? provider)
    {
        return ((ISpanFormattable)_valueOrThrow).TryFormat(destination, out charsWritten, format, provider);
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
    TypeCode IConvertible.GetTypeCode() => ((IConvertible)((TimeOnly)_valueOrThrow).ToDateTime()).GetTypeCode();

    /// <inheritdoc/>
    bool IConvertible.ToBoolean(IFormatProvider? provider) => ((IConvertible)((TimeOnly)_valueOrThrow).ToDateTime()).ToBoolean(provider);

    /// <inheritdoc/>
    byte IConvertible.ToByte(IFormatProvider? provider) => ((IConvertible)((TimeOnly)_valueOrThrow).ToDateTime()).ToByte(provider);

    /// <inheritdoc/>
    char IConvertible.ToChar(IFormatProvider? provider) => ((IConvertible)((TimeOnly)_valueOrThrow).ToDateTime()).ToChar(provider);

    /// <inheritdoc/>
    DateTime IConvertible.ToDateTime(IFormatProvider? provider) => ((IConvertible)((TimeOnly)_valueOrThrow).ToDateTime()).ToDateTime(provider);

    /// <inheritdoc/>
    decimal IConvertible.ToDecimal(IFormatProvider? provider) => ((IConvertible)((TimeOnly)_valueOrThrow).ToDateTime()).ToDecimal(provider);

    /// <inheritdoc/>
    double IConvertible.ToDouble(IFormatProvider? provider) => ((IConvertible)((TimeOnly)_valueOrThrow).ToDateTime()).ToDouble(provider);

    /// <inheritdoc/>
    short IConvertible.ToInt16(IFormatProvider? provider) => ((IConvertible)((TimeOnly)_valueOrThrow).ToDateTime()).ToInt16(provider);

    /// <inheritdoc/>
    int IConvertible.ToInt32(IFormatProvider? provider) => ((IConvertible)((TimeOnly)_valueOrThrow).ToDateTime()).ToInt32(provider);

    /// <inheritdoc/>
    long IConvertible.ToInt64(IFormatProvider? provider) => ((IConvertible)((TimeOnly)_valueOrThrow).ToDateTime()).ToInt64(provider);

    /// <inheritdoc/>
    sbyte IConvertible.ToSByte(IFormatProvider? provider) => ((IConvertible)((TimeOnly)_valueOrThrow).ToDateTime()).ToSByte(provider);

    /// <inheritdoc/>
    float IConvertible.ToSingle(IFormatProvider? provider) => ((IConvertible)((TimeOnly)_valueOrThrow).ToDateTime()).ToSingle(provider);

    /// <inheritdoc/>
    string IConvertible.ToString(IFormatProvider? provider) => ((IConvertible)((TimeOnly)_valueOrThrow).ToDateTime()).ToString(provider);

    /// <inheritdoc/>
    object IConvertible.ToType(Type conversionType, IFormatProvider? provider) => ((IConvertible)((TimeOnly)_valueOrThrow).ToDateTime()).ToType(conversionType, provider);

    /// <inheritdoc/>
    ushort IConvertible.ToUInt16(IFormatProvider? provider) => ((IConvertible)((TimeOnly)_valueOrThrow).ToDateTime()).ToUInt16(provider);

    /// <inheritdoc/>
    uint IConvertible.ToUInt32(IFormatProvider? provider) => ((IConvertible)((TimeOnly)_valueOrThrow).ToDateTime()).ToUInt32(provider);

    /// <inheritdoc/>
    ulong IConvertible.ToUInt64(IFormatProvider? provider) => ((IConvertible)((TimeOnly)_valueOrThrow).ToDateTime()).ToUInt64(provider);

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString() => _valueOrThrow.ToString();
}
