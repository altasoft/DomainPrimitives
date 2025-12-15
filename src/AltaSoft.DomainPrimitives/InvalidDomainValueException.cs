using System;
using System.ComponentModel;

namespace AltaSoft.DomainPrimitives;

/// <summary>
/// Represents an exception thrown when a value does not conform to the constraints or rules
/// defined within a specific domain context.
/// </summary>
public class InvalidDomainValueException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidDomainValueException"/> class with a specific error message.
    /// </summary>
    /// <param name="message">The error message that describes the reason for the exception.</param>
    /// <param name="type">The <see cref="Type"/> of the domain primitive that failed validation.</param>
    /// <param name="value">The underlying value that caused the failure, or <c>null</c> if not applicable.</param>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public InvalidDomainValueException(string message, Type type, object? value) : base(GenerateErrorMessage(message, type, value))
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidDomainValueException"/> class with a specific error message.
    /// </summary>
    /// <param name="message">The error message that describes the reason for the exception.</param>
    /// <param name="type">The <see cref="Type"/> of the domain primitive that failed validation.</param>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public InvalidDomainValueException(string message, Type type) : base(GenerateErrorMessage(message, type))
    {
    }

    /// <summary>
    /// Creates an <see cref="InvalidDomainValueException"/> that indicates the domain value has not been initialized.
    /// </summary>
    /// <param name="type">The <see cref="Type"/> of the domain primitive that is not initialized.</param>
    /// <returns>An <see cref="InvalidDomainValueException"/> describing the not-initialized error.</returns>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static InvalidDomainValueException NotInitializedException(Type type)
    {
        return new InvalidDomainValueException("The domain value has not been initialized", type);
    }

    /// <summary>
    /// Creates an <see cref="InvalidDomainValueException"/> for string length range violations.
    /// </summary>
    /// <param name="type">The <see cref="Type"/> of the domain primitive.</param>
    /// <param name="value">The string value that caused the violation.</param>
    /// <param name="min">The minimum allowed length.</param>
    /// <param name="max">The maximum allowed length.</param>
    /// <returns>An <see cref="InvalidDomainValueException"/> describing the range violation.</returns>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static InvalidDomainValueException StringRangeException(Type type, string value, int min, int max)
    {
        return new InvalidDomainValueException($"String length is out of range {min}..{max}", type, value);
    }

    /// <summary>
    /// Creates an <see cref="InvalidDomainValueException"/> for numeric limit exceeded errors.
    /// </summary>
    /// <param name="type">The <see cref="Type"/> of the domain primitive.</param>
    /// <param name="value">The numeric value that exceeded the limit.</param>
    /// <param name="underlyingTypeName">The name of the underlying primitive type.</param>
    /// <returns>An <see cref="InvalidDomainValueException"/> describing the limit exceeded error.</returns>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static InvalidDomainValueException LimitExceededException(Type type, int value, string underlyingTypeName)
    {
        return new InvalidDomainValueException($"The value has exceeded a {underlyingTypeName} limit", type, value);
    }

    /// <summary>
    /// Generates the error message for the <see cref="InvalidDomainValueException"/> including the underlying value.
    /// </summary>
    /// <param name="message">The main error message.</param>
    /// <param name="type">The <see cref="Type"/> of the domain primitive.</param>
    /// <param name="value">The underlying value that caused the error, or <c>null</c>.</param>
    /// <returns>A formatted error message string.</returns>
    private static string GenerateErrorMessage(string message, Type type, object? value)
    {
        var typeName = type.FullName ?? type.Name;
        var strValue = value switch
        {
            null => "(null)",
            string s => $"\"{s}\"",
            _ => value.ToString()
        };
        return $"Cannot create instance of '{typeName}'. {message}. Value: {strValue}";
    }

    /// <summary>
    /// Generates the error message for the <see cref="InvalidDomainValueException"/> without the underlying value.
    /// </summary>
    /// <param name="message">The main error message.</param>
    /// <param name="type">The <see cref="Type"/> of the domain primitive.</param>
    /// <returns>A formatted error message string.</returns>
    private static string GenerateErrorMessage(string message, Type type)
    {
        var typeName = type.FullName ?? type.Name;
        return $"Cannot create instance of '{typeName}'. {message}.";
    }
}
