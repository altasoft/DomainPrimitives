using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace AltaSoft.DomainPrimitives;

/// <summary>
/// Provides parsing helpers for <see cref="TimeOnly"/> values.
/// </summary>
public static class TimeOnlyExtensions
{
    internal static readonly string[] s_acceptedFormats =
    [
        "HH:mm:ss",
        "HH:mm:sszzz",   // 15:00:00+04:00
        "HH:mm:ssz",     // 15:00:00Z
        "HH:mm:ss'+'",   // 15:00:00+  (bare plus)
    ];

    /// <summary>
    /// Provides parsing helpers for <see cref="TimeOnly"/> values.
    /// </summary>
    extension(TimeOnly)
    {
        /// <summary>
        /// Attempts to parse the specified text into a <see cref="TimeOnly"/> value using an exact format first,
        /// and optionally falls back to standard time formats.
        /// </summary>
        /// <param name="value">The text to parse.</param>
        /// <param name="format">The exact time format to match.</param>
        /// <param name="allowStandardFormats">
        /// <see langword="true"/> to allow fallback parsing with standard formats when exact parsing fails;
        /// otherwise, <see langword="false"/>.
        /// </param>
        /// <param name="provider">An object that supplies culture-specific formatting information.</param>
        /// <param name="result">
        /// When this method returns, contains the parsed <see cref="TimeOnly"/> value if parsing succeeded;
        /// otherwise, the default value.
        /// </param>
        /// <returns><see langword="true"/> if parsing succeeded; otherwise, <see langword="false"/>.</returns>
        public static bool TryParseFlexible(
            [NotNullWhen(true)] string? value,
            [NotNullWhen(true), StringSyntax(StringSyntaxAttribute.DateTimeFormat)]
            string? format,
            bool allowStandardFormats,
            IFormatProvider? provider,
            out TimeOnly result)
        {
            if (TimeOnly.TryParseExact(value, format, provider, DateTimeStyles.None, out result))
                return true;

            if (allowStandardFormats)
            {
                if (TimeOnly.TryParse(value, provider, DateTimeStyles.None, out result))
                    return true;
            }

            result = default;
            return false;
        }

        /// <summary>
        /// Parses the specified text into a <see cref="TimeOnly"/> value using an exact format first,
        /// and falls back to standard time parsing when exact parsing fails.
        /// </summary>
        /// <param name="value">The text to parse.</param>
        /// <param name="format">The exact time format to match.</param>
        /// <param name="allowStandardFormats">
        /// <see langword="true"/> to allow fallback parsing with standard time formats when exact parsing fails;
        /// otherwise, <see langword="false"/>.
        /// </param>
        /// <param name="provider">An object that supplies culture-specific formatting information.</param>
        /// <returns>The parsed <see cref="TimeOnly"/> value.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="FormatException"><paramref name="value"/> is not in a recognized format.</exception>
        public static TimeOnly ParseFlexible(
            string value,
            [StringSyntax(StringSyntaxAttribute.DateTimeFormat)] string? format,
            bool allowStandardFormats,
            IFormatProvider? provider)
        {
            if (TimeOnlyExtensions.TryParseFlexible(value, format, allowStandardFormats, provider, out var result))
                return result;

            throw new FormatException($"The value '{value}' is not in a recognized format '{format}'.");
        }

        /// <summary>
        /// Attempts to parse the specified text into a <see cref="TimeOnly"/> value using standard time formats,
        /// falling back to a fixed set of accepted <see cref="DateTimeOffset"/> formats and extracting the time of day.
        /// </summary>
        /// <param name="value">The text to parse.</param>
        /// <param name="provider">An object that supplies culture-specific formatting information.</param>
        /// <param name="result">
        /// When this method returns, contains the parsed <see cref="TimeOnly"/> value if parsing succeeded;
        /// otherwise, the default value.
        /// </param>
        /// <returns><see langword="true"/> if parsing succeeded; otherwise, <see langword="false"/>.</returns>
        public static bool TryParseFlexible(
            [NotNullWhen(true)] string? value,
            IFormatProvider? provider,
            out TimeOnly result)
        {
            if (TimeOnly.TryParse(value, provider, DateTimeStyles.None, out result))
                return true;

            if (DateTimeOffset.TryParseExact(value, s_acceptedFormats, provider, DateTimeStyles.None, out var dt))
            {
                result = TimeOnly.FromTimeSpan(dt.TimeOfDay);
                return true;
            }

            result = default;
            return false;
        }

        /// <summary>
        /// Parses the specified text into a <see cref="TimeOnly"/> value using standard time formats,
        /// falling back to a fixed set of accepted <see cref="DateTimeOffset"/> formats and extracting the time of day.
        /// </summary>
        /// <param name="value">The text to parse.</param>
        /// <param name="provider">An object that supplies culture-specific formatting information.</param>
        /// <returns>The parsed <see cref="TimeOnly"/> value.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="FormatException"><paramref name="value"/> is not in a recognized format.</exception>
        public static TimeOnly ParseFlexible(string value, IFormatProvider? provider)
        {
            if (TimeOnlyExtensions.TryParseFlexible(value, provider, out var result))
                return result;

            throw new FormatException($"The value '{value}' is not in a recognized format.");
        }
    }
}
