using System;

namespace AltaSoft.DomainPrimitives;

/// <summary>
/// Validation attribute to assert a string property, field or parameter does not exceed a maximum length
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class StringLengthAttribute : Attribute
{
    /// <summary>
    /// Initializes a new instance of the <see cref="StringLengthAttribute"/> class.
    /// </summary>
    /// <param name="minimumLength">The minimum length allowed for the string.</param>
    /// <param name="maximumLength">The maximum length allowed for the string.</param>
    /// <param name="validate">Indicates whether the string length should be validated.</param>
    public StringLengthAttribute(int minimumLength, int maximumLength, bool validate = true)
    {
        MinimumLength = minimumLength;
        MaximumLength = maximumLength;
        Validate = validate;
    }

    /// <summary>
    /// Gets the maximum length allowed for the string.
    /// </summary>
    public int MaximumLength { get; }

    /// <summary>
    /// Gets the minimum length allowed for the string.
    /// </summary>
    public int MinimumLength { get; }

    /// <summary>
    /// Gets a value indicating whether the string length should be validated.
    /// </summary>
    public bool Validate { get; }
}
