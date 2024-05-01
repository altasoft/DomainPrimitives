using System;

namespace AltaSoft.DomainPrimitives;

/// <summary>
/// Validation attribute to assert a string property, field or parameter does not exceed a maximum length
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class StringLengthAttribute : Attribute
{
    /// <summary>
    /// Constructor that accepts the minimum and maximum lengths of the string.
    /// </summary>
    /// <param name="minimumLength">The minimum length, inclusive. It may not be negative.</param>
    /// <param name="maximumLength">The maximum length, inclusive. It may not be negative.</param>
    public StringLengthAttribute(int minimumLength, int maximumLength)
    {
        MinimumLength = minimumLength;
        MaximumLength = maximumLength;
    }

    /// <summary>
    /// Gets the maximum acceptable length of the string
    /// </summary>
    public int MaximumLength { get; }

    /// <summary>
    /// Gets or sets the minimum acceptable length of the string
    /// </summary>
    public int MinimumLength { get; set; }
}
