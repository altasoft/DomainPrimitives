using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Xml;

// ReSharper disable StringLiteralTypo
// ReSharper disable CommentTypo
// ReSharper disable UnusedMember.Global

namespace AltaSoft.DomainPrimitives;

/// <summary>
/// Provides extension methods for converting various data types to their XML string representations.
/// </summary>
public static class ToXmlStringExt
{
    /// <summary>
    /// Converts a <see cref="DateTime" /> value to its XML string representation in the format "yyyy-MM-ddTHH:mm:sszzz".
    /// </summary>
    /// <param name="value">The DateTime value to convert.</param>
    /// <returns>The XML string representation of the DateTime value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string ToXmlString(this DateTime value) => value.ToString("yyyy-MM-ddTHH:mm:sszzz", CultureInfo.InvariantCulture);

    /// <summary>
    /// Converts a <see cref="DateOnly" /> value to its XML string representation in the format "yyyy-MM-dd".
    /// </summary>
    /// <param name="value">The DateOnly value to convert.</param>
    /// <returns>The XML string representation of the DateOnly value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string ToXmlString(this DateOnly value) => value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);

    /// <summary>
    /// Converts a <see cref="TimeOnly" /> value to its XML string representation in the format "HH:mm:ss".
    /// </summary>
    /// <param name="value">The TimeOnly value to convert.</param>
    /// <returns>The XML string representation of the TimeOnly value.</returns>

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string ToXmlString(this TimeOnly value)
    {
        // We avoid constructing a local DateTime for an arbitrary date to prevent
        // possible exceptions on DST transition days (invalid or ambiguous local times).
        var timePart = value.ToString("HH:mm:ss", CultureInfo.InvariantCulture);

        // Derive the current local offset from UTC in a DST-safe way using the current instant.
        var offset = TimeZoneInfo.Local.GetUtcOffset(DateTime.UtcNow);
        var offsetSign = offset < TimeSpan.Zero ? "-" : "+";
        var absoluteOffset = offset.Duration();
        var offsetPart = absoluteOffset.ToString(@"hh\:mm", CultureInfo.InvariantCulture);

        return $"{timePart}{offsetSign}{offsetPart}";
    }

    /// <summary>
    /// Converts a <see cref="DateTimeOffset" /> value to its XML string representation
    /// </summary>
    /// <param name="value">The TimeOnly value to convert.</param>
    /// <returns>The XML string representation of the TimeOnly value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string ToXmlString(this DateTimeOffset value) => XmlConvert.ToString(value);

    /// <summary>
    /// Converts a <see cref="TimeSpan" /> value to its XML string representation
    /// </summary>
    /// <param name="value">The TimeOnly value to convert.</param>
    /// <returns>The XML string representation of the TimeOnly value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string ToXmlString(this TimeSpan value) => XmlConvert.ToString(value);

    /// <summary>
    /// Converts a <see cref="byte" /> value to its XML string representation.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>The XML string representation of the integer value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string ToXmlString(this byte value)
    {
        return value.ToString(null, NumberFormatInfo.InvariantInfo);
    }

    /// <summary>
    /// Converts a <see cref="sbyte" /> value to its XML string representation.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>The XML string representation of the integer value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string ToXmlString(this sbyte value)
    {
        return value.ToString(null, NumberFormatInfo.InvariantInfo);
    }

    /// <summary>
    /// Converts a <see cref="short" /> value to its XML string representation.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>The XML string representation of the integer value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string ToXmlString(this short value)
    {
        return value.ToString(null, NumberFormatInfo.InvariantInfo);
    }

    /// <summary>
    /// Converts a <see cref="ushort" /> value to its XML string representation.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>The XML string representation of the integer value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string ToXmlString(this ushort value)
    {
        return value.ToString(null, NumberFormatInfo.InvariantInfo);
    }

    /// <summary>
    /// Converts a <see cref="int" /> value to its XML string representation.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>The XML string representation of the integer value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string ToXmlString(this int value)
    {
        return value.ToString(null, NumberFormatInfo.InvariantInfo);
    }

    /// <summary>
    /// Converts a <see cref="uint" /> value to its XML string representation.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>The XML string representation of the integer value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string ToXmlString(this uint value)
    {
        return value.ToString(null, NumberFormatInfo.InvariantInfo);
    }

    /// <summary>
    /// Converts a <see cref="long" /> value to its XML string representation.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>The XML string representation of the integer value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string ToXmlString(this long value)
    {
        return value.ToString(null, NumberFormatInfo.InvariantInfo);
    }

    /// <summary>
    /// Converts a <see cref="ulong" /> value to its XML string representation.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>The XML string representation of the integer value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string ToXmlString(this ulong value)
    {
        return value.ToString(null, NumberFormatInfo.InvariantInfo);
    }

    /// <summary>
    /// Converts a <see cref="float" /> value to its XML string representation.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>The XML string representation of the integer value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string ToXmlString(this float value)
    {
        return value.ToString(null, NumberFormatInfo.InvariantInfo);
    }

    /// <summary>
    /// Converts a <see cref="double" /> value to its XML string representation.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>The XML string representation of the integer value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string ToXmlString(this double value)
    {
        return value.ToString(null, NumberFormatInfo.InvariantInfo);
    }

    /// <summary>
    /// Converts a <see cref="decimal" /> value to its XML string representation.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>The XML string representation of the integer value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string ToXmlString(this decimal value)
    {
        return value.ToString(null, NumberFormatInfo.InvariantInfo);
    }

    /// <summary>
    /// Converts a <see cref="Guid" /> value to its XML string representation.
    /// </summary>
    /// <param name="value">The decimal value to convert.</param>
    /// <returns>The XML string representation of the decimal value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string ToXmlString(this Guid value) => value.ToString();

    /// <summary>
    /// Converts a <see cref="bool" /> value to its XML string representation.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>The XML string representation of the boolean value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string ToXmlString(this bool value)
    {
        return value ? "true" : "false";
    }

    /// <summary>
    /// Converts a <see cref="char" /> value to its XML string representation.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>The XML string representation of the character value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string ToXmlString(this char value)
    {
        return value.ToString();
    }
}
