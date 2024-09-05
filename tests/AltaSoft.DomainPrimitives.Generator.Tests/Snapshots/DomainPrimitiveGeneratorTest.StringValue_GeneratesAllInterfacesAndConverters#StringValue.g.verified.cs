﻿//HintName: StringValue.g.cs
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

[JsonConverter(typeof(StringValueJsonConverter))]
[TypeConverter(typeof(StringValueTypeConverter))]
[UnderlyingPrimitiveType(typeof(string))]
[DebuggerDisplay("{_value}")]
public partial class StringValue : IEquatable<StringValue>
        , IComparable
        , IComparable<StringValue>
        , IParsable<StringValue>
        , IConvertible
{
    /// <inheritdoc/>
     public Type GetUnderlyingPrimitiveType() => typeof(string);
    /// <inheritdoc/>
     public object GetUnderlyingPrimitiveValue() => (string)this;

    private string _valueOrThrow => _isInitialized ? _value : throw new InvalidDomainValueException("The domain value has not been initialized", this);
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly string _value;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly bool _isInitialized;

    /// <summary>
    /// Initializes a new instance of the <see cref="StringValue"/> class by validating the specified <see cref="string"/> value using <see cref="Validate"/> static method.
    /// </summary>
    /// <param name="value">The value to be validated.</param>
    public StringValue(string value) : this(value, true)
    {
    }

    private StringValue(string value, bool validate) 
    {
        if (validate)
        {
            ValidateOrThrow(value);
        }
        _value = value;
        _isInitialized = true;
    }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    /// <inheritdoc/>
    [Obsolete("Domain primitive cannot be created using empty Constructor", true)]
    public StringValue()
    {
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    /// <summary>
    /// Tries to create an instance of AsciiString from the specified value.
    /// </summary>
    /// <param name="value">The value to create StringValue from</param>
    /// <param name="result">When this method returns, contains the created StringValue if the conversion succeeded, or null if the conversion failed.</param>
    /// <returns>true if the conversion succeeded; otherwise, false.</returns>
    public static bool TryCreate(string value, [NotNullWhen(true)] out StringValue? result)
    {
        return TryCreate(value, out result, out _);
    }

    /// <summary>
    /// Tries to create an instance of AsciiString from the specified value.
    /// </summary>
    /// <param name="value">The value to create StringValue from</param>
    /// <param name="result">When this method returns, contains the created StringValue if the conversion succeeded, or null if the conversion failed.</param>
    /// <param name="errorMessage">When this method returns, contains the error message if the conversion failed; otherwise, null.</param>
    /// <returns>true if the conversion succeeded; otherwise, false.</returns>
    public static bool TryCreate(string value,[NotNullWhen(true)]  out StringValue? result, [NotNullWhen(false)]  out string? errorMessage)
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
    public void ValidateOrThrow(string value)
    {
        var result = Validate(value);
        if (!result.IsValid)
        	throw new InvalidDomainValueException(result.ErrorMessage, this);
    }


    /// <summary>
    /// Gets the character at the specified index.
    /// </summary>
    public char this[int i]
    {
        get => _value[i];
    }

    /// <summary>
    /// Gets the character at the specified index.
    /// </summary>
    public char this[Index index]
    {
        get => _value[index];
    }

    /// <summary>
    /// Gets the substring by specified range.
    /// </summary>
    public string this[Range range]
    {
        get => _value[range];
    }

    /// <summary>
    /// Gets the number of characters.
    /// </summary>
    /// <returns>The number of characters in underlying string value.</returns>
    public int Length => _value.Length;

    /// <summary>
    /// Returns a substring of this string.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string Substring(int startIndex, int length) => _value.Substring(startIndex, length);

    /// <summary>
    /// Returns a substring of this string.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string Substring(int startIndex) => _value.Substring(startIndex);

    /// <summary>
    /// Returns the entire string as an array of characters.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public char[] ToCharArray() => _value.ToCharArray();

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool Equals(object? obj) => obj is StringValue other && Equals(other);
    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(StringValue? other)
    {
        if (other is null || !_isInitialized || !other._isInitialized)
            return false;
        return _value.Equals(other._value);
    }
    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(StringValue? left, StringValue? right)
    {
        if (ReferenceEquals(left, right))
            return true;
        if (left is null || right is null)
            return false;
        return left.Equals(right);
    }
    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(StringValue? left, StringValue? right) => !(left == right);

    /// <inheritdoc/>
    public int CompareTo(object? obj)
    {
        if (obj is null)
            return 1;

        if (obj is StringValue c)
            return CompareTo(c);

        throw new ArgumentException("Object is not a StringValue", nameof(obj));
    }

    /// <inheritdoc/>
    public int CompareTo(StringValue? other)
    {
        if (other is null || !other._isInitialized)
            return 1;
        if (!_isInitialized)
            return -1;
        return _value.CompareTo(other._value);
    }

    /// <summary>
    /// Implicit conversion from <see cref = "string"/> (nullable) to <see cref = "StringValue"/> (nullable)
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [return: NotNullIfNotNull(nameof(value))]
    public static implicit operator StringValue?(string? value) => value is null ? null : new(value);

    /// <summary>
    /// Implicit conversion from <see cref = "StringValue"/> (nullable) to <see cref = "string"/> (nullable)
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [return: NotNullIfNotNull(nameof(value))]
    public static implicit operator string?(StringValue? value) => value is null ? null : (string?)value._valueOrThrow;

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static StringValue Parse(string s, IFormatProvider? provider) => s;

    /// <inheritdoc/>
    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out StringValue result)
    {
        if (s is null)
        {
            result = default;
            return false;
        }

        return StringValue.TryCreate(s, out result);
    }

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override int GetHashCode() => _valueOrThrow.GetHashCode();

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    TypeCode IConvertible.GetTypeCode() => ((IConvertible)(String)_valueOrThrow).GetTypeCode();

    /// <inheritdoc/>
    bool IConvertible.ToBoolean(IFormatProvider? provider) => ((IConvertible)(String)_valueOrThrow).ToBoolean(provider);

    /// <inheritdoc/>
    byte IConvertible.ToByte(IFormatProvider? provider) => ((IConvertible)(String)_valueOrThrow).ToByte(provider);

    /// <inheritdoc/>
    char IConvertible.ToChar(IFormatProvider? provider) => ((IConvertible)(String)_valueOrThrow).ToChar(provider);

    /// <inheritdoc/>
    DateTime IConvertible.ToDateTime(IFormatProvider? provider) => ((IConvertible)(String)_valueOrThrow).ToDateTime(provider);

    /// <inheritdoc/>
    decimal IConvertible.ToDecimal(IFormatProvider? provider) => ((IConvertible)(String)_valueOrThrow).ToDecimal(provider);

    /// <inheritdoc/>
    double IConvertible.ToDouble(IFormatProvider? provider) => ((IConvertible)(String)_valueOrThrow).ToDouble(provider);

    /// <inheritdoc/>
    short IConvertible.ToInt16(IFormatProvider? provider) => ((IConvertible)(String)_valueOrThrow).ToInt16(provider);

    /// <inheritdoc/>
    int IConvertible.ToInt32(IFormatProvider? provider) => ((IConvertible)(String)_valueOrThrow).ToInt32(provider);

    /// <inheritdoc/>
    long IConvertible.ToInt64(IFormatProvider? provider) => ((IConvertible)(String)_valueOrThrow).ToInt64(provider);

    /// <inheritdoc/>
    sbyte IConvertible.ToSByte(IFormatProvider? provider) => ((IConvertible)(String)_valueOrThrow).ToSByte(provider);

    /// <inheritdoc/>
    float IConvertible.ToSingle(IFormatProvider? provider) => ((IConvertible)(String)_valueOrThrow).ToSingle(provider);

    /// <inheritdoc/>
    string IConvertible.ToString(IFormatProvider? provider) => ((IConvertible)(String)_valueOrThrow).ToString(provider);

    /// <inheritdoc/>
    object IConvertible.ToType(Type conversionType, IFormatProvider? provider) => ((IConvertible)(String)_valueOrThrow).ToType(conversionType, provider);

    /// <inheritdoc/>
    ushort IConvertible.ToUInt16(IFormatProvider? provider) => ((IConvertible)(String)_valueOrThrow).ToUInt16(provider);

    /// <inheritdoc/>
    uint IConvertible.ToUInt32(IFormatProvider? provider) => ((IConvertible)(String)_valueOrThrow).ToUInt32(provider);

    /// <inheritdoc/>
    ulong IConvertible.ToUInt64(IFormatProvider? provider) => ((IConvertible)(String)_valueOrThrow).ToUInt64(provider);

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString() => _valueOrThrow.ToString();
}
