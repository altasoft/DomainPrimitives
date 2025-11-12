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
}
