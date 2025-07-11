﻿//HintName: TimeSpanValue.g.cs
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

[JsonConverter(typeof(TimeSpanValueJsonConverter))]
[TypeConverter(typeof(TimeSpanValueTypeConverter))]
[UnderlyingPrimitiveType(typeof(TimeSpan))]
[DebuggerDisplay("{_value}")]
public readonly partial struct TimeSpanValue : IEquatable<TimeSpanValue>
        , IComparable
        , IComparable<TimeSpanValue>
        , IComparisonOperators<TimeSpanValue, TimeSpanValue, bool>
        , ISpanFormattable
        , IParsable<TimeSpanValue>
#if NET8_0_OR_GREATER
        , IUtf8SpanFormattable
#endif
{
    /// <inheritdoc/>
     public Type GetUnderlyingPrimitiveType() => typeof(TimeSpan);
    /// <inheritdoc/>
     public object GetUnderlyingPrimitiveValue() => (TimeSpan)this;

    private TimeSpan _valueOrThrow => _isInitialized ? _value : throw new InvalidDomainValueException("The domain value has not been initialized", this);
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly TimeSpan _value;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly bool _isInitialized;

    /// <summary>
    /// Initializes a new instance of the <see cref="TimeSpanValue"/> class by validating the specified <see cref="TimeSpan"/> value using <see cref="Validate"/> static method.
    /// </summary>
    /// <param name="value">The value to be validated.</param>
    public TimeSpanValue(TimeSpan value) : this(value, true)
    {
    }

    private TimeSpanValue(TimeSpan value, bool validate) 
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
    public TimeSpanValue()
    {
    }

    /// <summary>
    /// Tries to create an instance of AsciiString from the specified value.
    /// </summary>
    /// <param name="value">The value to create TimeSpanValue from</param>
    /// <param name="result">When this method returns, contains the created TimeSpanValue if the conversion succeeded, or null if the conversion failed.</param>
    /// <returns>true if the conversion succeeded; otherwise, false.</returns>
    public static bool TryCreate(TimeSpan value, [NotNullWhen(true)] out TimeSpanValue? result)
    {
        return TryCreate(value, out result, out _);
    }

    /// <summary>
    /// Tries to create an instance of AsciiString from the specified value.
    /// </summary>
    /// <param name="value">The value to create TimeSpanValue from</param>
    /// <param name="result">When this method returns, contains the created TimeSpanValue if the conversion succeeded, or null if the conversion failed.</param>
    /// <param name="errorMessage">When this method returns, contains the error message if the conversion failed; otherwise, null.</param>
    /// <returns>true if the conversion succeeded; otherwise, false.</returns>
    public static bool TryCreate(TimeSpan value, [NotNullWhen(true)]  out TimeSpanValue? result, [NotNullWhen(false)]  out string? errorMessage)
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
    public void ValidateOrThrow(TimeSpan value)
    {
        var result = Validate(value);
        if (!result.IsValid)
        	throw new InvalidDomainValueException(result.ErrorMessage, this);
    }


    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool Equals(object? obj) => obj is TimeSpanValue other && Equals(other);
    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(TimeSpanValue other)
    {
        if (!_isInitialized || !other._isInitialized)
            return false;
        return _value.Equals(other._value);
    }
    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(TimeSpanValue left, TimeSpanValue right) => left.Equals(right);
    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(TimeSpanValue left, TimeSpanValue right) => !(left == right);

    /// <inheritdoc/>
    public int CompareTo(object? obj)
    {
        if (obj is null)
            return 1;

        if (obj is TimeSpanValue c)
            return CompareTo(c);

        throw new ArgumentException("Object is not a TimeSpanValue", nameof(obj));
    }

    /// <inheritdoc/>
    public int CompareTo(TimeSpanValue other)
    {
        if (!other._isInitialized)
            return 1;
        if (!_isInitialized)
            return -1;
        return _value.CompareTo(other._value);
    }

    /// <summary>
    /// Implicit conversion from <see cref = "TimeSpan"/> to <see cref = "TimeSpanValue"/>
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator TimeSpanValue(TimeSpan value) => new(value);

    /// <summary>
    /// Implicit conversion from <see cref = "TimeSpan"/> (nullable) to <see cref = "TimeSpanValue"/> (nullable)
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [return: NotNullIfNotNull(nameof(value))]
    public static implicit operator TimeSpanValue?(TimeSpan? value) => value is null ? null : new(value.Value);

    /// <summary>
    /// Implicit conversion from <see cref = "TimeSpanValue"/> to <see cref = "TimeSpan"/>
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator TimeSpan(TimeSpanValue value) => (TimeSpan)value._valueOrThrow;

    /// <summary>
    /// Implicit conversion from <see cref = "TimeSpanValue"/> (nullable) to <see cref = "TimeSpan"/> (nullable)
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [return: NotNullIfNotNull(nameof(value))]
    public static implicit operator TimeSpan?(TimeSpanValue? value) => value is null ? null : (TimeSpan?)value.Value._valueOrThrow;

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <(TimeSpanValue left, TimeSpanValue right) => left._valueOrThrow < right._valueOrThrow;

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <=(TimeSpanValue left, TimeSpanValue right) => left._valueOrThrow <= right._valueOrThrow;

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >(TimeSpanValue left, TimeSpanValue right) => left._valueOrThrow > right._valueOrThrow;

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >=(TimeSpanValue left, TimeSpanValue right) => left._valueOrThrow >= right._valueOrThrow;


    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TimeSpanValue Parse(string s, IFormatProvider? provider) => TimeSpan.Parse(s, provider);

    /// <inheritdoc/>
    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out TimeSpanValue result)
    {
        if (!TimeSpan.TryParse(s, provider, out var value))
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
    public override string ToString() => _valueOrThrow.ToString();
}
