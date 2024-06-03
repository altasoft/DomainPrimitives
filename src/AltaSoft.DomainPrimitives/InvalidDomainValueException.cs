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
    /// <param name="instance">actual instance of domain primitive</param>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public InvalidDomainValueException(string message, IDomainValue instance) : base(GenerateErrorMessage(message, instance))
    {
    }

    /// <summary>
    /// Generates the error message for the <see cref="InvalidDomainValueException"/>.
    /// </summary>
    /// <param name="message">The error message that describes the reason for the exception.</param>
    /// <param name="value">The actual value of the domain primitive.</param>
    /// <returns>The generated error message.</returns>
    private static string GenerateErrorMessage(string message, IDomainValue value)
    {
        var type = value.GetType();
        var typeName = type.FullName ?? type.Name;
        return $"Cannot create instance of '{typeName}'. {message}";
    }
}
