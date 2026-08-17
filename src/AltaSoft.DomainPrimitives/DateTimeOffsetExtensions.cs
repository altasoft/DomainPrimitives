using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace AltaSoft.DomainPrimitives;

/// <summary>
/// Provides parsing helpers for <see cref="DateTimeOffset"/> values.
/// </summary>
public static class DateTimeOffsetExtensions
{
    /// <summary>
    /// Provides parsing helpers for <see cref="DateTimeOffset"/> values.
    /// </summary>
    extension(DateTimeOffset)
    {
        /// <summary>
        /// Attempts to parse the specified text into a <see cref="DateTimeOffset"/> value using an exact format first,
        /// and optionally falls back to standard date and time formats.
        /// </summary>
        /// <param name="value">The text to parse.</param>
        /// <param name="format">The exact date and time format to match.</param>
        /// <param name="allowStandardFormats">
        /// <see langword="true"/> to allow fallback parsing with standard formats when exact parsing fails;
        /// otherwise, <see langword="false"/>.
        /// </param>
        /// <param name="provider">An object that supplies culture-specific formatting information.</param>
        /// <param name="result">
        /// When this method returns, contains the parsed <see cref="DateTimeOffset"/> value if parsing succeeded;
        /// otherwise, the default value.
        /// </param>
        /// <returns><see langword="true"/> if parsing succeeded; otherwise, <see langword="false"/>.</returns>
        public static bool TryParseFlexible(
            [NotNullWhen(true)] string? value,
            [NotNullWhen(true), StringSyntax("DateTimeFormat")]
            string? format,
            bool allowStandardFormats,
            IFormatProvider? provider,
            out DateTimeOffset result)
        {
            if (DateTimeOffset.TryParseExact(value, format, provider, DateTimeStyles.None, out result))
                return true;

            if (allowStandardFormats)
            {
                if (DateTimeOffset.TryParse(value, provider, DateTimeStyles.None, out result))
                    return true;
            }

            result = default;
            return false;
        }

        /// <summary>
        /// Parses the specified text into a <see cref="DateTimeOffset"/> value using an exact format first,
        /// and falls back to standard date and time parsing when exact parsing fails.
        /// </summary>
        /// <param name="value">The text to parse.</param>
        /// <param name="format">The exact date and time format to match.</param>
        /// <param name="allowStandardFormats">
        /// Reserved for API symmetry with <see cref="DateTimeOffsetExtensions.TryParseFlexible"/>.
        /// </param>
        /// <param name="provider">An object that supplies culture-specific formatting information.</param>
        /// <returns>The parsed <see cref="DateTimeOffset"/> value.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="FormatException"><paramref name="value"/> is not in a recognized format.</exception>
        public static DateTimeOffset ParseFlexible(
            string value,
            [StringSyntax("DateTimeFormat")] string? format,
            bool allowStandardFormats,
            IFormatProvider? provider)
        {
            if (DateTimeOffsetExtensions.TryParseFlexible(value, format, allowStandardFormats, provider, out var result))
                return result;

            throw new FormatException($"The value '{value}' is not in a recognized format '{format}'.");
        }

        /// <summary>
        /// Attempts to parse the specified text into a <see cref="DateTimeOffset"/> value using standard date and time formats.
        /// </summary>
        /// <param name="value">The text to parse.</param>
        /// <param name="provider">An object that supplies culture-specific formatting information.</param>
        /// <param name="result">
        /// When this method returns, contains the parsed <see cref="DateTimeOffset"/> value if parsing succeeded;
        /// otherwise, the default value.
        /// </param>
        /// <returns><see langword="true"/> if parsing succeeded; otherwise, <see langword="false"/>.</returns>
        public static bool TryParseFlexible(
            [NotNullWhen(true)] string? value,
            IFormatProvider? provider,
            out DateTimeOffset result)
        {
            return DateTimeOffset.TryParse(value, provider, DateTimeStyles.None, out result);
        }

        /// <summary>
        /// Parses the specified text into a <see cref="DateTimeOffset"/> value using standard date and time formats.
        /// </summary>
        /// <param name="value">The text to parse.</param>
        /// <param name="provider">An object that supplies culture-specific formatting information.</param>
        /// <returns>The parsed <see cref="DateTimeOffset"/> value.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="FormatException"><paramref name="value"/> is not in a recognized format.</exception>
        public static DateTimeOffset ParseFlexible(string value, IFormatProvider? provider)
        {
            if (DateTimeOffsetExtensions.TryParseFlexible(value, provider, out var result))
                return result;

            throw new FormatException($"The value '{value}' is not in a recognized format.");
        }
    }
}
