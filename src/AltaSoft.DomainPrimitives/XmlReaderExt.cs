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
    /// <param name="allowStandardFormats">Indicates whether to allow fallback parsing with standard formats when exact parsing fails.</param>
    /// <returns>
    /// A <see cref="DateTime"/> value parsed from the current element's content.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DateTime ReadElementContentAsDateTime(this XmlReader reader, string serializationFormat, bool allowStandardFormats)
    {
        var str = reader.ReadElementContentAsString();

        return DateTimeExtensions.ParseFlexible(str, serializationFormat, allowStandardFormats, CultureInfo.InvariantCulture);
    }

    /// <summary>
    /// Reads the content of the current XML element as a <see cref="TimeOnly"/> value.
    /// </summary>
    /// <param name="reader">The <see cref="XmlReader"/> instance.</param>
    /// <param name="serializationFormat">serialization format to be used</param>
    /// <param name="allowStandardFormats">Indicates whether to allow fallback parsing with standard formats when exact parsing fails.</param>
    /// <returns>
    /// A <see cref="TimeOnly"/> value parsed from the current element's content.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TimeOnly ReadElementContentAsTimeOnly(this XmlReader reader, string serializationFormat, bool allowStandardFormats)
    {
        var str = reader.ReadElementContentAsString();

        return TimeOnlyExtensions.ParseFlexible(str, serializationFormat, allowStandardFormats, CultureInfo.InvariantCulture);
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

        return DateOnlyExtensions.ParseFlexible(str, CultureInfo.InvariantCulture);
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

        return TimeOnlyExtensions.ParseFlexible(str, CultureInfo.InvariantCulture);
    }

    /// <summary>
    /// Reads the content of the current XML element as a <see cref="DateOnly"/> value.
    /// </summary>
    /// <param name="reader">The <see cref="XmlReader"/> instance.</param>
    /// <param name="serializationFormat">serialization format to be used</param>
    /// <param name="allowStandardFormats"></param>
    /// <returns>
    /// A <see cref="DateOnly"/> value parsed from the current element's content.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DateOnly ReadElementContentAsDateOnly(this XmlReader reader, string serializationFormat, bool allowStandardFormats)
    {
        var str = reader.ReadElementContentAsString();

        return DateOnlyExtensions.ParseFlexible(str, serializationFormat, allowStandardFormats, CultureInfo.InvariantCulture);
    }

    /// <summary>
    /// Reads the content of the current XML element as a  <see cref="DateTimeOffset" /> value.
    /// </summary>
    /// <param name="reader">The <see cref="XmlReader"/> instance.</param>
    /// <param name="serializationFormat">serialization format to be used</param>
    /// <param name="allowStandardFormats">Indicates whether to allow fallback parsing with standard formats when exact parsing fails.</param>
    /// <returns>
    /// A <see cref="DateTimeOffset"/> value parsed from the current element's content.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DateTimeOffset ReadElementContentAsDateTimeOffset(this XmlReader reader, string serializationFormat, bool allowStandardFormats)
    {
        var str = reader.ReadElementContentAsString();

        return DateTimeOffsetExtensions.ParseFlexible(str, serializationFormat, allowStandardFormats, CultureInfo.InvariantCulture);
    }

    /// <summary>
    /// Reads the content of the current XML element as a  <see cref="TimeSpan" /> value.
    /// </summary>
    /// <param name="reader">The <see cref="XmlReader"/> instance.</param>
    /// <param name="serializationFormat">serialization format to be used</param>
    /// <param name="allowStandardFormats">Indicates whether to allow fallback parsing with standard formats when exact parsing fails.</param>
    /// <returns>
    /// A <see cref="TimeSpan"/> value parsed from the current element's content.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TimeSpan ReadElementContentAsTimeSpan(this XmlReader reader, string serializationFormat, bool allowStandardFormats)
    {
        var str = reader.ReadElementContentAsString();

        return TimeSpanExtensions.ParseFlexible(str, serializationFormat, allowStandardFormats, CultureInfo.InvariantCulture);
    }
}
