using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace AltaSoft.DomainPrimitives;

/// <summary>
/// Provides parsing helpers for <see cref="TimeSpan"/> values.
/// </summary>
public static class TimeSpanExtensions
{
    /// <summary>
    /// Provides parsing helpers for <see cref="TimeSpan"/> values.
    /// </summary>
    extension(TimeSpan)
    {
        /// <summary>
        /// Attempts to parse the specified text into a <see cref="TimeSpan"/> value using an exact format first,
        /// and optionally falls back to standard time span formats.
        /// </summary>
        /// <param name="value">The text to parse.</param>
        /// <param name="format">The exact time span format to match.</param>
        /// <param name="allowStandardFormats">
        /// <see langword="true"/> to allow fallback parsing with standard formats when exact parsing fails;
        /// otherwise, <see langword="false"/>.
        /// </param>
        /// <param name="provider">An object that supplies culture-specific formatting information.</param>
        /// <param name="result">
        /// When this method returns, contains the parsed <see cref="TimeSpan"/> value if parsing succeeded;
        /// otherwise, the default value.
        /// </param>
        /// <returns><see langword="true"/> if parsing succeeded; otherwise, <see langword="false"/>.</returns>
        public static bool TryParseFlexible(
            [NotNullWhen(true)] string? value,
            [NotNullWhen(true), StringSyntax("TimeSpanFormat")]
            string? format,
            bool allowStandardFormats,
            IFormatProvider? provider,
            out TimeSpan result)
        {
            if (TimeSpan.TryParseExact(value, format, provider, TimeSpanStyles.None, out result))
                return true;

            if (allowStandardFormats)
            {
                if (TimeSpan.TryParse(value, provider, out result))
                    return true;
            }

            result = default;
            return false;
        }

        /// <summary>
        /// Parses the specified text into a <see cref="TimeSpan"/> value using an exact format first,
        /// and falls back to standard time span parsing when exact parsing fails.
        /// </summary>
        /// <param name="value">The text to parse.</param>
        /// <param name="format">The exact time span format to match.</param>
        /// <param name="allowStandardFormats">
        /// <see langword="true"/> to allow fallback parsing with standard time span formats when exact parsing
        /// fails; otherwise, <see langword="false"/>.
        /// </param>
        /// <param name="provider">An object that supplies culture-specific formatting information.</param>
        /// <returns>The parsed <see cref="TimeSpan"/> value.</returns>B
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="FormatException"><paramref name="value"/> is not in a recognized format.</exception>
        public static TimeSpan ParseFlexible(
            string value,
            [StringSyntax("TimeSpanFormat")] string? format,
            bool allowStandardFormats,
            IFormatProvider? provider)
        {
            if (TimeSpanExtensions.TryParseFlexible(value, format, allowStandardFormats, provider, out var result))
                return result;

            throw new FormatException($"The value '{value}' is not in a recognized format '{format}'.");
        }

        /// <summary>
        /// Attempts to parse the specified text into a <see cref="TimeSpan"/> value using standard time span formats.
        /// </summary>
        /// <param name="value">The text to parse.</param>
        /// <param name="provider">An object that supplies culture-specific formatting information.</param>
        /// <param name="result">
        /// When this method returns, contains the parsed <see cref="TimeSpan"/> value if parsing succeeded;
        /// otherwise, the default value.
        /// </param>
        /// <returns><see langword="true"/> if parsing succeeded; otherwise, <see langword="false"/>.</returns>
        public static bool TryParseFlexible(
            [NotNullWhen(true)] string? value,
            IFormatProvider? provider,
            out TimeSpan result)
        {
            return TimeSpan.TryParse(value, provider, out result);
        }

        /// <summary>
        /// Parses the specified text into a <see cref="TimeSpan"/> value using standard time span formats.
        /// </summary>
        /// <param name="value">The text to parse.</param>
        /// <param name="provider">An object that supplies culture-specific formatting information.</param>
        /// <returns>The parsed <see cref="TimeSpan"/> value.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="FormatException"><paramref name="value"/> is not in a recognized format.</exception>
        public static TimeSpan ParseFlexible(string value, IFormatProvider? provider)
        {
            if (TimeSpanExtensions.TryParseFlexible(value, provider, out var result))
                return result;

            throw new FormatException($"The value '{value}' is not in a recognized format.");
        }
    }
}
