﻿//HintName: GuidValue.g.cs
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

[JsonConverter(typeof(GuidValueJsonConverter))]
[TypeConverter(typeof(GuidValueTypeConverter))]
[UnderlyingPrimitiveType(typeof(Guid))]
[DebuggerDisplay("{_value}")]
public readonly partial struct GuidValue : IEquatable<GuidValue>
        , IComparable
        , IComparable<GuidValue>
        , ISpanFormattable
        , IParsable<GuidValue>
#if NET8_0_OR_GREATER
        , IUtf8SpanFormattable
#endif
{
    /// <inheritdoc/>
     public Type GetUnderlyingPrimitiveType() => typeof(Guid);
    /// <inheritdoc/>
     public object GetUnderlyingPrimitiveValue() => (Guid)this;

    private Guid _valueOrThrow => _isInitialized ? _value : throw new InvalidDomainValueException("The domain value has not been initialized");
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly Guid _value;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly bool _isInitialized;

    /// <summary>
    /// Initializes a new instance of the <see cref="GuidValue"/> class by validating the specified <see cref="Guid"/> value using <see cref="Validate"/> static method.
    /// </summary>
    /// <param name="value">The value to be validated.</param>
    public GuidValue(Guid value)
    {
        Validate(value);
        _value = value;
        _isInitialized = true;
    }

    /// <inheritdoc/>
    [Obsolete("Domain primitive cannot be created using empty Ctor", true)]
    public GuidValue()
    {
    }

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool Equals(object? obj) => obj is GuidValue other && Equals(other);
    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(GuidValue other) => _valueOrThrow == other._valueOrThrow;
    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(GuidValue left, GuidValue right) => left.Equals(right);
    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(GuidValue left, GuidValue right) => !(left == right);

    /// <inheritdoc/>
    public int CompareTo(object? obj)
    {
        if (obj is null)
            return 1;

        if (obj is GuidValue c)
            return CompareTo(c);

        throw new ArgumentException("Object is not a GuidValue", nameof(obj));
    }

    /// <inheritdoc/>
    public int CompareTo(GuidValue other) => _valueOrThrow.CompareTo(other._valueOrThrow);

    /// <summary>
    /// Implicit conversion from <see cref = "Guid"/> to <see cref = "GuidValue"/>
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator GuidValue(Guid value) => new(value);

    /// <summary>
    /// Implicit conversion from <see cref = "Guid"/> (nullable) to <see cref = "GuidValue"/> (nullable)
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [return: NotNullIfNotNull(nameof(value))]
    public static implicit operator GuidValue?(Guid? value) => value is null ? null : new(value.Value);

    /// <summary>
    /// Implicit conversion from <see cref = "GuidValue"/> to <see cref = "Guid"/>
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Guid(GuidValue value) => (Guid)value._valueOrThrow;

    /// <summary>
    /// Implicit conversion from <see cref = "GuidValue"/> (nullable) to <see cref = "Guid"/> (nullable)
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [return: NotNullIfNotNull(nameof(value))]
    public static implicit operator Guid?(GuidValue? value) => value is null ? null : (Guid?)value.Value._valueOrThrow;

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static GuidValue Parse(string s, IFormatProvider? provider) => Guid.Parse(s, provider);

    /// <inheritdoc/>
    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out GuidValue result)
    {
        if (!Guid.TryParse(s, provider, out var value))
        {
            result = default;
            return false;
        }

        try
        {
            result = new GuidValue(value);
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
    public override string ToString() => _valueOrThrow.ToString();
}
