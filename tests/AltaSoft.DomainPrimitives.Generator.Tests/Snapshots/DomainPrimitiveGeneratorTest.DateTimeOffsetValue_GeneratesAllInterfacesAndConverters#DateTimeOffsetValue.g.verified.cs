﻿//HintName: DateTimeOffsetValue.g.cs
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

[JsonConverter(typeof(DateTimeOffsetValueJsonConverter))]
[TypeConverter(typeof(DateTimeOffsetValueTypeConverter))]
[UnderlyingPrimitiveType(typeof(DateTimeOffset))]
[DebuggerDisplay("{_value}")]
public readonly partial struct DateTimeOffsetValue : IEquatable<DateTimeOffsetValue>
        , IComparable
        , IComparable<DateTimeOffsetValue>
        , IComparisonOperators<DateTimeOffsetValue, DateTimeOffsetValue, bool>
        , ISpanFormattable
        , IParsable<DateTimeOffsetValue>
        , IConvertible
#if NET8_0_OR_GREATER
        , IUtf8SpanFormattable
#endif
{
    /// <inheritdoc/>
     public Type GetUnderlyingPrimitiveType() => typeof(DateTimeOffset);
    /// <inheritdoc/>
     public object GetUnderlyingPrimitiveValue() => (DateTimeOffset)this;

    private DateTimeOffset _valueOrThrow => _isInitialized ? _value : throw new InvalidDomainValueException("The domain value has not been initialized", this);
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly DateTimeOffset _value;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly bool _isInitialized;

    /// <summary>
    /// Initializes a new instance of the <see cref="DateTimeOffsetValue"/> class by validating the specified <see cref="DateTimeOffset"/> value using <see cref="Validate"/> static method.
    /// </summary>
    /// <param name="value">The value to be validated.</param>
    public DateTimeOffsetValue(DateTimeOffset value) : this(value, true)
    {
    }

    private DateTimeOffsetValue(DateTimeOffset value, bool validate) 
    {
        if (validate)
        {
            ValidateOrThrow(value);
        }
        _value = value;
        _isInitialized = true;
    }

    /// <inheritdoc/>
    [Obsolete("Domain primitive cannot be created using empty Constructor", true)]
    public DateTimeOffsetValue()
    {
    }

    /// <summary>
    /// Tries to create an instance of AsciiString from the specified value.
    /// </summary>
    /// <param name="value">The value to create DateTimeOffsetValue from</param>
    /// <param name="result">When this method returns, contains the created DateTimeOffsetValue if the conversion succeeded, or null if the conversion failed.</param>
    /// <returns>true if the conversion succeeded; otherwise, false.</returns>
    public static bool TryCreate(DateTimeOffset value, [NotNullWhen(true)] out DateTimeOffsetValue? result)
    {
        return TryCreate(value, out result, out _);
    }

    /// <summary>
    /// Tries to create an instance of AsciiString from the specified value.
    /// </summary>
    /// <param name="value">The value to create DateTimeOffsetValue from</param>
    /// <param name="result">When this method returns, contains the created DateTimeOffsetValue if the conversion succeeded, or null if the conversion failed.</param>
    /// <param name="errorMessage">When this method returns, contains the error message if the conversion failed; otherwise, null.</param>
    /// <returns>true if the conversion succeeded; otherwise, false.</returns>
    public static bool TryCreate(DateTimeOffset value,[NotNullWhen(true)]  out DateTimeOffsetValue? result, [NotNullWhen(false)]  out string? errorMessage)
    {
        var validationResult = Validate(value);
        if (!validationResult.IsValid)
        {
            result = null;
            errorMessage = validationResult.ErrorMessage;
            return false;
        }

        result = new (value, false);
        errorMessage = null;
        return true;
    }

    /// <summary>
    ///  Validates the specified value and throws an exception if it is not valid.
    /// </summary>
    /// <param name="value">The value to validate</param>
    /// <exception cref="InvalidDomainValueException">Thrown when the value is not valid.</exception>
    public void ValidateOrThrow(DateTimeOffset value)
    {
        var result = Validate(value);
        if (!result.IsValid)
        	throw new InvalidDomainValueException(result.ErrorMessage, this);
    }


    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool Equals(object? obj) => obj is DateTimeOffsetValue other && Equals(other);
    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(DateTimeOffsetValue other)
    {
        if (!_isInitialized || !other._isInitialized)
            return false;
        return _value.Equals(other._value);
    }
    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(DateTimeOffsetValue left, DateTimeOffsetValue right) => left.Equals(right);
    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(DateTimeOffsetValue left, DateTimeOffsetValue right) => !(left == right);

    /// <inheritdoc/>
    public int CompareTo(object? obj)
    {
        if (obj is null)
            return 1;

        if (obj is DateTimeOffsetValue c)
            return CompareTo(c);

        throw new ArgumentException("Object is not a DateTimeOffsetValue", nameof(obj));
    }

    /// <inheritdoc/>
    public int CompareTo(DateTimeOffsetValue other)
    {
        if (!other._isInitialized)
            return 1;
        if (!_isInitialized)
            return -1;
        return _value.CompareTo(other._value);
    }

    /// <summary>
    /// Implicit conversion from <see cref = "DateTimeOffset"/> to <see cref = "DateTimeOffsetValue"/>
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator DateTimeOffsetValue(DateTimeOffset value) => new(value);

    /// <summary>
    /// Implicit conversion from <see cref = "DateTimeOffset"/> (nullable) to <see cref = "DateTimeOffsetValue"/> (nullable)
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [return: NotNullIfNotNull(nameof(value))]
    public static implicit operator DateTimeOffsetValue?(DateTimeOffset? value) => value is null ? null : new(value.Value);

    /// <summary>
    /// Implicit conversion from <see cref = "DateTimeOffsetValue"/> to <see cref = "DateTimeOffset"/>
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator DateTimeOffset(DateTimeOffsetValue value) => (DateTimeOffset)value._valueOrThrow;

    /// <summary>
    /// Implicit conversion from <see cref = "DateTimeOffsetValue"/> (nullable) to <see cref = "DateTimeOffset"/> (nullable)
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [return: NotNullIfNotNull(nameof(value))]
    public static implicit operator DateTimeOffset?(DateTimeOffsetValue? value) => value is null ? null : (DateTimeOffset?)value.Value._valueOrThrow;

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <(DateTimeOffsetValue left, DateTimeOffsetValue right) => left._valueOrThrow < right._valueOrThrow;

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <=(DateTimeOffsetValue left, DateTimeOffsetValue right) => left._valueOrThrow <= right._valueOrThrow;

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >(DateTimeOffsetValue left, DateTimeOffsetValue right) => left._valueOrThrow > right._valueOrThrow;

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >=(DateTimeOffsetValue left, DateTimeOffsetValue right) => left._valueOrThrow >= right._valueOrThrow;


    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DateTimeOffsetValue Parse(string s, IFormatProvider? provider) => DateTimeOffset.Parse(s, provider);

    /// <inheritdoc/>
    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out DateTimeOffsetValue result)
    {
        if (!DateTimeOffset.TryParse(s, provider, out var value))
        {
            result = default;
            return false;
        }

        if (TryCreate(value, out var created))
        {
            result = created.Value;
            return true;
        }

        result = default;
        return false;
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
    TypeCode IConvertible.GetTypeCode() => ((IConvertible)(DateTimeOffset)_valueOrThrow).GetTypeCode();

    /// <inheritdoc/>
    bool IConvertible.ToBoolean(IFormatProvider? provider) => ((IConvertible)(DateTimeOffset)_valueOrThrow).ToBoolean(provider);

    /// <inheritdoc/>
    byte IConvertible.ToByte(IFormatProvider? provider) => ((IConvertible)(DateTimeOffset)_valueOrThrow).ToByte(provider);

    /// <inheritdoc/>
    char IConvertible.ToChar(IFormatProvider? provider) => ((IConvertible)(DateTimeOffset)_valueOrThrow).ToChar(provider);

    /// <inheritdoc/>
    DateTime IConvertible.ToDateTime(IFormatProvider? provider) => ((IConvertible)(DateTimeOffset)_valueOrThrow).ToDateTime(provider);

    /// <inheritdoc/>
    decimal IConvertible.ToDecimal(IFormatProvider? provider) => ((IConvertible)(DateTimeOffset)_valueOrThrow).ToDecimal(provider);

    /// <inheritdoc/>
    double IConvertible.ToDouble(IFormatProvider? provider) => ((IConvertible)(DateTimeOffset)_valueOrThrow).ToDouble(provider);

    /// <inheritdoc/>
    short IConvertible.ToInt16(IFormatProvider? provider) => ((IConvertible)(DateTimeOffset)_valueOrThrow).ToInt16(provider);

    /// <inheritdoc/>
    int IConvertible.ToInt32(IFormatProvider? provider) => ((IConvertible)(DateTimeOffset)_valueOrThrow).ToInt32(provider);

    /// <inheritdoc/>
    long IConvertible.ToInt64(IFormatProvider? provider) => ((IConvertible)(DateTimeOffset)_valueOrThrow).ToInt64(provider);

    /// <inheritdoc/>
    sbyte IConvertible.ToSByte(IFormatProvider? provider) => ((IConvertible)(DateTimeOffset)_valueOrThrow).ToSByte(provider);

    /// <inheritdoc/>
    float IConvertible.ToSingle(IFormatProvider? provider) => ((IConvertible)(DateTimeOffset)_valueOrThrow).ToSingle(provider);

    /// <inheritdoc/>
    string IConvertible.ToString(IFormatProvider? provider) => ((IConvertible)(DateTimeOffset)_valueOrThrow).ToString(provider);

    /// <inheritdoc/>
    object IConvertible.ToType(Type conversionType, IFormatProvider? provider) => ((IConvertible)(DateTimeOffset)_valueOrThrow).ToType(conversionType, provider);

    /// <inheritdoc/>
    ushort IConvertible.ToUInt16(IFormatProvider? provider) => ((IConvertible)(DateTimeOffset)_valueOrThrow).ToUInt16(provider);

    /// <inheritdoc/>
    uint IConvertible.ToUInt32(IFormatProvider? provider) => ((IConvertible)(DateTimeOffset)_valueOrThrow).ToUInt32(provider);

    /// <inheritdoc/>
    ulong IConvertible.ToUInt64(IFormatProvider? provider) => ((IConvertible)(DateTimeOffset)_valueOrThrow).ToUInt64(provider);

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString() => _valueOrThrow.ToString();
}
