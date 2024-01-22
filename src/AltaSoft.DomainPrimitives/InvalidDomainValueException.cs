using System;

namespace AltaSoft.DomainPrimitives;

/// <summary>
/// Represents an exception thrown when a value does not conform to the constraints or rules
/// defined within a specific domain context.
/// </summary>
public sealed class InvalidDomainValueException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidDomainValueException"/> class.
    /// </summary>
    public InvalidDomainValueException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidDomainValueException"/> class with a specific error message.
    /// </summary>
    /// <param name="message">The error message that describes the reason for the exception.</param>
    public InvalidDomainValueException(string message) : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidDomainValueException"/> class with a specific error message
    /// and a reference to the inner exception that caused this exception.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="innerException">The exception that is the cause of the current exception.</param>
    public InvalidDomainValueException(string message, Exception? innerException) : base(message, innerException)
    {
    }
}