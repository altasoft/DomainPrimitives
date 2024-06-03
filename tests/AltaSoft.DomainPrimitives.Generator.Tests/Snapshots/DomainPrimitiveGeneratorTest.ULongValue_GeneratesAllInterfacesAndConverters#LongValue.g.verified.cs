﻿//HintName: LongValue.g.cs
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

[JsonConverter(typeof(LongValueJsonConverter))]
[TypeConverter(typeof(LongValueTypeConverter))]
[UnderlyingPrimitiveType(typeof(ulong))]
[DebuggerDisplay("{_value}")]
public readonly partial struct LongValue : IEquatable<LongValue>
        , IComparable
        , IComparable<LongValue>
        , IAdditionOperators<LongValue, LongValue, LongValue>
        , ISubtractionOperators<LongValue, LongValue, LongValue>
        , IMultiplyOperators<LongValue, LongValue, LongValue>
        , IDivisionOperators<LongValue, LongValue, LongValue>
        , IModulusOperators<LongValue, LongValue, LongValue>
        , IComparisonOperators<LongValue, LongValue, bool>
        , IParsable<LongValue>
        , IConvertible
#if NET8_0_OR_GREATER
        , IUtf8SpanFormattable
#endif
{
    /// <inheritdoc/>
     public Type GetUnderlyingPrimitiveType() => typeof(ulong);
    /// <inheritdoc/>
     public object GetUnderlyingPrimitiveValue() => (ulong)this;

    private ulong _valueOrThrow => _isInitialized ? _value : throw new InvalidDomainValueException("The domain value has not been initialized", this);
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly ulong _value;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly bool _isInitialized;

    /// <summary>
    /// Initializes a new instance of the <see cref="LongValue"/> class by validating the specified <see cref="ulong"/> value using <see cref="Validate"/> static method.
    /// </summary>
    /// <param name="value">The value to be validated.</param>
    public LongValue(ulong value) : this(value, true)
    {
    }

    private LongValue(ulong value, bool validate) 
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
    public LongValue()
    {
    }

    /// <summary>
    /// Tries to create an instance of AsciiString from the specified value.
    /// </summary>
    /// <param name="value">The value to create LongValue from</param>
    /// <param name="result">When this method returns, contains the created LongValue if the conversion succeeded, or null if the conversion failed.</param>
    /// <returns>true if the conversion succeeded; otherwise, false.</returns>
    public static bool TryCreate(ulong value, [NotNullWhen(true)] out LongValue? result)
    {
        return TryCreate(value, out result, out _);
    }

    /// <summary>
    /// Tries to create an instance of AsciiString from the specified value.
    /// </summary>
    /// <param name="value">The value to create LongValue from</param>
    /// <param name="result">When this method returns, contains the created LongValue if the conversion succeeded, or null if the conversion failed.</param>
    /// <param name="errorMessage">When this method returns, contains the error message if the conversion failed; otherwise, null.</param>
    /// <returns>true if the conversion succeeded; otherwise, false.</returns>
    public static bool TryCreate(ulong value,[NotNullWhen(true)]  out LongValue? result, [NotNullWhen(false)]  out string? errorMessage)
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
    public void ValidateOrThrow(ulong value)
    {
        var result = Validate(value);
        if (!result.IsValid)
        	throw new InvalidDomainValueException(result.ErrorMessage, this);
    }


    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool Equals(object? obj) => obj is LongValue other && Equals(other);
    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(LongValue other)
    {
        if (!_isInitialized || !other._isInitialized)
            return false;
        return _value.Equals(other._value);
    }
    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(LongValue left, LongValue right) => left.Equals(right);
    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(LongValue left, LongValue right) => !(left == right);

    /// <inheritdoc/>
    public int CompareTo(object? obj)
    {
        if (obj is null)
            return 1;

        if (obj is LongValue c)
            return CompareTo(c);

        throw new ArgumentException("Object is not a LongValue", nameof(obj));
    }

    /// <inheritdoc/>
    public int CompareTo(LongValue other)
    {
        if (!other._isInitialized)
            return 1;
        if (!_isInitialized)
            return -1;
        return _value.CompareTo(other._value);
    }

    /// <summary>
    /// Implicit conversion from <see cref = "ulong"/> to <see cref = "LongValue"/>
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator LongValue(ulong value) => new(value);

    /// <summary>
    /// Implicit conversion from <see cref = "ulong"/> (nullable) to <see cref = "LongValue"/> (nullable)
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [return: NotNullIfNotNull(nameof(value))]
    public static implicit operator LongValue?(ulong? value) => value is null ? null : new(value.Value);

    /// <summary>
    /// Implicit conversion from <see cref = "LongValue"/> to <see cref = "ulong"/>
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator ulong(LongValue value) => (ulong)value._valueOrThrow;

    /// <summary>
    /// Implicit conversion from <see cref = "LongValue"/> (nullable) to <see cref = "ulong"/> (nullable)
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [return: NotNullIfNotNull(nameof(value))]
    public static implicit operator ulong?(LongValue? value) => value is null ? null : (ulong?)value.Value._valueOrThrow;

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LongValue operator +(LongValue left, LongValue right) => new(left._valueOrThrow + right._valueOrThrow);

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LongValue operator -(LongValue left, LongValue right) => new(left._valueOrThrow - right._valueOrThrow);

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LongValue operator *(LongValue left, LongValue right) => new(left._valueOrThrow * right._valueOrThrow);

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LongValue operator /(LongValue left, LongValue right) => new(left._valueOrThrow / right._valueOrThrow);

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LongValue operator %(LongValue left, LongValue right) => new(left._valueOrThrow % right._valueOrThrow);

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <(LongValue left, LongValue right) => left._valueOrThrow < right._valueOrThrow;

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <=(LongValue left, LongValue right) => left._valueOrThrow <= right._valueOrThrow;

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >(LongValue left, LongValue right) => left._valueOrThrow > right._valueOrThrow;

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >=(LongValue left, LongValue right) => left._valueOrThrow >= right._valueOrThrow;


    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LongValue Parse(string s, IFormatProvider? provider) => ulong.Parse(s, provider);

    /// <inheritdoc/>
    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out LongValue result)
    {
        if (!ulong.TryParse(s, provider, out var value))
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
    TypeCode IConvertible.GetTypeCode() => ((IConvertible)(UInt64)_valueOrThrow).GetTypeCode();

    /// <inheritdoc/>
    bool IConvertible.ToBoolean(IFormatProvider? provider) => ((IConvertible)(UInt64)_valueOrThrow).ToBoolean(provider);

    /// <inheritdoc/>
    byte IConvertible.ToByte(IFormatProvider? provider) => ((IConvertible)(UInt64)_valueOrThrow).ToByte(provider);

    /// <inheritdoc/>
    char IConvertible.ToChar(IFormatProvider? provider) => ((IConvertible)(UInt64)_valueOrThrow).ToChar(provider);

    /// <inheritdoc/>
    DateTime IConvertible.ToDateTime(IFormatProvider? provider) => ((IConvertible)(UInt64)_valueOrThrow).ToDateTime(provider);

    /// <inheritdoc/>
    decimal IConvertible.ToDecimal(IFormatProvider? provider) => ((IConvertible)(UInt64)_valueOrThrow).ToDecimal(provider);

    /// <inheritdoc/>
    double IConvertible.ToDouble(IFormatProvider? provider) => ((IConvertible)(UInt64)_valueOrThrow).ToDouble(provider);

    /// <inheritdoc/>
    short IConvertible.ToInt16(IFormatProvider? provider) => ((IConvertible)(UInt64)_valueOrThrow).ToInt16(provider);

    /// <inheritdoc/>
    int IConvertible.ToInt32(IFormatProvider? provider) => ((IConvertible)(UInt64)_valueOrThrow).ToInt32(provider);

    /// <inheritdoc/>
    long IConvertible.ToInt64(IFormatProvider? provider) => ((IConvertible)(UInt64)_valueOrThrow).ToInt64(provider);

    /// <inheritdoc/>
    sbyte IConvertible.ToSByte(IFormatProvider? provider) => ((IConvertible)(UInt64)_valueOrThrow).ToSByte(provider);

    /// <inheritdoc/>
    float IConvertible.ToSingle(IFormatProvider? provider) => ((IConvertible)(UInt64)_valueOrThrow).ToSingle(provider);

    /// <inheritdoc/>
    string IConvertible.ToString(IFormatProvider? provider) => ((IConvertible)(UInt64)_valueOrThrow).ToString(provider);

    /// <inheritdoc/>
    object IConvertible.ToType(Type conversionType, IFormatProvider? provider) => ((IConvertible)(UInt64)_valueOrThrow).ToType(conversionType, provider);

    /// <inheritdoc/>
    ushort IConvertible.ToUInt16(IFormatProvider? provider) => ((IConvertible)(UInt64)_valueOrThrow).ToUInt16(provider);

    /// <inheritdoc/>
    uint IConvertible.ToUInt32(IFormatProvider? provider) => ((IConvertible)(UInt64)_valueOrThrow).ToUInt32(provider);

    /// <inheritdoc/>
    ulong IConvertible.ToUInt64(IFormatProvider? provider) => ((IConvertible)(UInt64)_valueOrThrow).ToUInt64(provider);

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString() => _valueOrThrow.ToString();
}
