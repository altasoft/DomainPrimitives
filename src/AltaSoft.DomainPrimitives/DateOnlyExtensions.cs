using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace AltaSoft.DomainPrimitives;

/// <summary>
/// Provides parsing helpers for <see cref="DateOnly"/> values.
/// </summary>
public static class DateOnlyExtensions
{
    /// <summary>
    /// Provides parsing helpers for <see cref="DateOnly"/> values.
    /// </summary>
    extension(DateOnly)
    {
        /// <summary>
        /// Attempts to parse the specified text into a <see cref="DateOnly"/> value using an exact format first,
        /// then falls back to standard date formats, and finally to general <see cref="DateTime"/> parsing.
        /// </summary>
        /// <param name="value">The text to parse.</param>
        /// <param name="format">The exact date format to match.</param>
        /// <param name="allowStandardFormats">
        /// <see langword="true"/> to allow fallback parsing with standard and general <see cref="DateTime"/> formats
        /// when exact parsing fails; otherwise, <see langword="false"/>.
        /// </param>
        /// <param name="provider">An object that supplies culture-specific formatting information.</param>
        /// <param name="result">
        /// When this method returns, contains the parsed <see cref="DateOnly"/> value if parsing succeeded;
        /// otherwise, the default value.
        /// </param>
        /// <returns><see langword="true"/> if parsing succeeded; otherwise, <see langword="false"/>.</returns>
        public static bool TryParseFlexible(
            [NotNullWhen(true)] string? value,
            [NotNullWhen(true), StringSyntax("DateTimeFormat")]
            string? format,
            bool allowStandardFormats,
            IFormatProvider? provider,
            out DateOnly result)
        {
            if (DateOnly.TryParseExact(value, format, provider, DateTimeStyles.None, out result))
                return true;

            if (allowStandardFormats)
            {
                if (DateOnly.TryParse(value, provider, DateTimeStyles.None, out result))
                    return true;

                if (DateTime.TryParse(value, provider, DateTimeStyles.None, out var dt))
                {
                    result = DateOnly.FromDateTime(dt);
                    return true;
                }
            }

            result = default;
            return false;
        }

        /// <summary>
        /// Parses the specified text into a <see cref="DateOnly"/> value using an exact format first,
        /// then falls back to standard date formats, and finally to general <see cref="DateTime"/> parsing.
        /// </summary>
        /// <param name="value">The text to parse.</param>
        /// <param name="format">The exact date format to match.</param>
        /// <param name="allowStandardFormats">
        /// Reserved for API symmetry with <see cref="DateOnlyExtensions.TryParseFlexible"/>.
        /// </param>
        /// <param name="provider">An object that supplies culture-specific formatting information.</param>
        /// <returns>The parsed <see cref="DateOnly"/> value.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="FormatException"><paramref name="value"/> is not in a recognized format.</exception>
        public static DateOnly ParseFlexible(
            string value,
            [StringSyntax("DateTimeFormat")] string? format,
            bool allowStandardFormats,
            IFormatProvider? provider)
        {
            if (DateOnlyExtensions.TryParseFlexible(value, format, allowStandardFormats, provider, out var result))
                return result;

            throw new FormatException($"The value '{value}' is not in a recognized format '{format}'.");
        }

        /// <summary>
        /// Attempts to parse the specified text into a <see cref="DateOnly"/> value using standard date formats,
        /// falling back to general <see cref="DateTime"/> parsing.
        /// </summary>
        /// <param name="value">The text to parse.</param>
        /// <param name="provider">An object that supplies culture-specific formatting information.</param>
        /// <param name="result">
        /// When this method returns, contains the parsed <see cref="DateOnly"/> value if parsing succeeded;
        /// otherwise, the default value.
        /// </param>
        /// <returns><see langword="true"/> if parsing succeeded; otherwise, <see langword="false"/>.</returns>
        public static bool TryParseFlexible(
            [NotNullWhen(true)] string? value,
            IFormatProvider? provider,
            out DateOnly result)
        {
            if (DateOnly.TryParse(value, provider, DateTimeStyles.None, out result))
                return true;

            if (DateTime.TryParse(value, provider, DateTimeStyles.None, out var dt))
            {
                result = DateOnly.FromDateTime(dt);
                return true;
            }

            result = default;
            return false;
        }

        /// <summary>
        /// Parses the specified text into a <see cref="DateOnly"/> value using standard date formats,
        /// falling back to general <see cref="DateTime"/> parsing.
        /// </summary>
        /// <param name="value">The text to parse.</param>
        /// <param name="provider">An object that supplies culture-specific formatting information.</param>
        /// <returns>The parsed <see cref="DateOnly"/> value.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="FormatException"><paramref name="value"/> is not in a recognized format.</exception>
        public static DateOnly ParseFlexible(string value, IFormatProvider? provider)
        {
            if (DateOnlyExtensions.TryParseFlexible(value, provider, out var result))
                return result;

            throw new FormatException($"The value '{value}' is not in a recognized format.");
        }
    }
}
