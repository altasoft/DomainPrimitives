using System;

// ReSharper disable UnusedMember.Global

namespace AltaSoft.DomainPrimitives;

/// <summary>
/// Provides general utility methods for various common operations.
/// </summary>
public static class Utils
{
    /// <summary>
    /// Converts a DateOnly value to a DateTime, with the time component set to the minimum value and kind set to local.
    /// </summary>
    /// <param name="value">The DateOnly value to convert.</param>
    /// <returns>A DateTime representation of the given DateOnly value.</returns>
    public static DateTime ToDateTime(this DateOnly value) => value.ToDateTime(TimeOnly.MinValue, DateTimeKind.Unspecified);

    /// <summary>
    /// Converts a TimeOnly value to a DateTime, with the date component set to the minimum value and kind set to local.
    /// </summary>
    /// <param name="value">The TimeOnly value to convert.</param>
    /// <returns>A DateTime representation of the given TimeOnly value.</returns>
    public static DateTime ToDateTime(this TimeOnly value) => new(value.Ticks, DateTimeKind.Unspecified);
}
