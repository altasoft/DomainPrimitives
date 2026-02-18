using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Xml;

// ReSharper disable UnusedMember.Global

namespace AltaSoft.DomainPrimitives;

/// <summary>
/// Provides extension methods for XmlReader and XmlWriter to simplify reading and writing of certain data types.
/// </summary>
public static class XmlReaderExt
{
    /// <summary>
    /// Reads the content of the current XML element as a <typeparamref name="T"/> value.
    /// </summary>
    /// <typeparam name="T">The type of value to parse, which must implement <see cref="IParsable{TSelf}"/>.</typeparam>
    /// <param name="reader">The <see cref="XmlReader"/> instance.</param>
    /// <returns>
    /// A <typeparamref name="T"/> value parsed from the current element's content.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T ReadElementContentAs<T>(this XmlReader reader) where T : IParsable<T>
    {
        return T.Parse(reader.ReadElementContentAsString(), CultureInfo.InvariantCulture);
    }

    /// <summary>
    /// Reads the content of the current XML element as a  <see cref="DateTime"/> value.
    /// </summary>
    /// <param name="reader">The <see cref="XmlReader"/> instance.</param>
    /// <param name="serializationFormat">serialization format to be used</param>
    /// <returns>
    /// A <see cref="DateTime"/> value parsed from the current element's content.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DateTime ReadElementContentAsDateTime(this XmlReader reader, string serializationFormat)
    {
        var str = reader.ReadElementContentAsString();

        if (DateTime.TryParseExact(str, serializationFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var result))
            return result;

        return DateTime.Parse(str, CultureInfo.InvariantCulture);
    }

    /// <summary>
    /// Reads the content of the current XML element as a <see cref="TimeOnly"/> value.
    /// </summary>
    /// <param name="reader">The <see cref="XmlReader"/> instance.</param>
    /// <param name="serializationFormat">serialization format to be used</param>
    /// <returns>
    /// A <see cref="TimeOnly"/> value parsed from the current element's content.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TimeOnly ReadElementContentAsTimeOnly(this XmlReader reader, string serializationFormat)
    {
        var str = reader.ReadElementContentAsString();

        if (TimeOnly.TryParseExact(str, serializationFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var result))
            return result;

        return TimeOnly.Parse(str, CultureInfo.InvariantCulture);
    }

    /// <summary>
    /// Reads the content of the current XML element as a <see cref="DateOnly"/> value.
    /// </summary>
    /// <param name="reader">The <see cref="XmlReader"/> instance.</param>
    /// <returns>
    /// A <see cref="DateOnly"/> value parsed from the current element's content.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DateOnly ReadElementContentAsDateOnly(this XmlReader reader)
    {
        var str = reader.ReadElementContentAsString();
        if (DateOnly.TryParse(str, CultureInfo.InvariantCulture, out var result))
            return result;

        return DateOnly.FromDateTime(DateTime.Parse(str, CultureInfo.InvariantCulture));
    }

    /// <summary>
    /// Reads the content of the current XML element as a <see cref="TimeOnly"/> value.
    /// </summary>
    /// <param name="reader">The <see cref="XmlReader"/> instance.</param>
    /// <returns>
    /// A <see cref="TimeOnly"/> value parsed from the current element's content.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TimeOnly ReadElementContentAsTimeOnly(this XmlReader reader)
    {
        var str = reader.ReadElementContentAsString();
        if (TimeOnly.TryParse(str, CultureInfo.InvariantCulture, out var result))
            return result;

        var dt = DateTimeOffset.ParseExact(str, s_acceptedFormats, CultureInfo.InvariantCulture, DateTimeStyles.None);
        return TimeOnly.FromTimeSpan(dt.TimeOfDay);

    }

    /// <summary>
    /// Reads the content of the current XML element as a <see cref="DateOnly"/> value.
    /// </summary>
    /// <param name="reader">The <see cref="XmlReader"/> instance.</param>
    /// <param name="serializationFormat">serialization format to be used</param>
    /// <returns>
    /// A <see cref="DateOnly"/> value parsed from the current element's content.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DateOnly ReadElementContentAsDateOnly(this XmlReader reader, string serializationFormat)
    {
        var str = reader.ReadElementContentAsString();

        if (DateOnly.TryParseExact(str, serializationFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var result))
            return result;

        if (DateOnly.TryParse(str, CultureInfo.InvariantCulture, out result))
            return result;

        return DateOnly.FromDateTime(DateTime.Parse(str, CultureInfo.InvariantCulture));
    }

    /// <summary>
    /// Reads the content of the current XML element as a  <see cref="DateTimeOffset" /> value.
    /// </summary>
    /// <param name="reader">The <see cref="XmlReader"/> instance.</param>
    /// <param name="serializationFormat">serialization format to be used</param>
    /// <returns>
    /// A <see cref="DateTimeOffset"/> value parsed from the current element's content.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DateTimeOffset ReadElementContentAsDateTimeOffset(this XmlReader reader, string serializationFormat)
    {
        var str = reader.ReadElementContentAsString();

        if (DateTimeOffset.TryParseExact(str, serializationFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var result))
            return result;

        return DateTimeOffset.Parse(str, CultureInfo.InvariantCulture);
    }

    /// <summary>
    /// Reads the content of the current XML element as a  <see cref="TimeSpan" /> value.
    /// </summary>
    /// <param name="reader">The <see cref="XmlReader"/> instance.</param>
    /// <param name="serializationFormat">serialization format to be used</param>
    /// <returns>
    /// A <see cref="TimeSpan"/> value parsed from the current element's content.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TimeSpan ReadElementContentAsTimeSpan(this XmlReader reader, string serializationFormat)
    {
        var str = reader.ReadElementContentAsString();

        if (TimeSpan.TryParseExact(str, serializationFormat, CultureInfo.InvariantCulture, TimeSpanStyles.None, out var result))
            return result;

        return TimeSpan.Parse(str, CultureInfo.InvariantCulture);
    }

    private static readonly string[] s_acceptedFormats =
    [
        "HH:mm:ss",
        "HH:mm:sszzz",   // 15:00:00+04:00
        "HH:mm:ssz",     // 15:00:00Z
        "HH:mm:ss'+'",   // 15:00:00+  (bare plus)
    ];
}
