using System;

namespace AltaSoft.DomainPrimitives;

/// <summary>
/// Defines a contract for domain-specific values ensuring type safety and constraints.
/// This interface serves as a foundation for encapsulating and validating domain-specific values.
/// </summary>
/// <typeparam name="T">The type of the domain value.</typeparam>
public interface IDomainValue<in T> : IDomainValue
    where T : IEquatable<T>, IComparable, IComparable<T>
{
    /// <summary>
    /// Validates the specified value against domain-specific rules.
    /// </summary>
    /// <param name="value">The value to be validated against domain constraints.</param>
    /// <exception cref="InvalidDomainValueException">Thrown when validation fails due to domain-specific constraints.</exception>
    static abstract void Validate(T value);

    /// <summary>
    /// Retrieves a string representation of the specified domain value.
    /// </summary>
    /// <param name="value">The domain value to be represented as a string.</param>
    /// <returns>A string representation of the domain value.</returns>
    static virtual string ToString(T value) => value.ToString() ?? string.Empty;
}

/// <summary>
/// Represents an interface for domain values.
/// </summary>
public interface IDomainValue
{
    /// <summary>
    /// Gets the underlying primitive type of the domain value.
    /// </summary>
    /// <returns>The underlying primitive type of the domain value.</returns>
    Type GetUnderlyingPrimitiveType();

    /// <summary>
    /// Gets the underlying primitive value of the domain value.
    /// </summary>
    /// <returns>The underlying primitive value of the domain value.</returns>
    object GetUnderlyingPrimitiveValue();
}
