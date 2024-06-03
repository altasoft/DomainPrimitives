using System.Diagnostics.CodeAnalysis;

namespace AltaSoft.DomainPrimitives;

/// <summary>
/// DomainPrimitive validation result.
/// </summary>
public readonly struct PrimitiveValidationResult
{
    /// <summary>
    /// Gets a value indicating whether the validation result is valid.
    /// </summary>
    [MemberNotNullWhen(false, nameof(ErrorMessage))]
    public bool IsValid { get; }

    /// <summary>
    /// Gets the value associated with the validation result.
    /// </summary>
    public string? ErrorMessage { get; }

    /// <summary>
    /// Creates a new instance of the <see cref="PrimitiveValidationResult"/> struct with a valid result.
    /// </summary>
    /// <returns>A new instance of the <see cref="PrimitiveValidationResult"/> struct with a valid result.</returns>
    public static readonly PrimitiveValidationResult Ok = new(true, null);

    /// <summary>
    /// Creates a new instance of the <see cref="PrimitiveValidationResult"/> struct with an error result.
    /// </summary>
    /// <param name="error">The error message associated with the result.</param>
    /// <returns>A new instance of the <see cref="PrimitiveValidationResult"/> struct with an error result.</returns>
    public static PrimitiveValidationResult Error(string error) => new(false, error);

    /// <summary>
    /// Implicitly converts a string value to a <see cref="PrimitiveValidationResult"/> with an error result.
    /// </summary>
    /// <param name="value">The string value representing the error message.</param>
    /// <returns>A <see cref="PrimitiveValidationResult"/> with an error result.</returns>
    public static implicit operator PrimitiveValidationResult(string value) => Error(value);

    /// <summary>
    /// Initializes a new instance of the <see cref="PrimitiveValidationResult"/> struct.
    /// </summary>
    /// <param name="isValid">A value indicating whether the validation result is valid.</param>
    /// <param name="errorMessage">The value associated with the validation result.</param>
    private PrimitiveValidationResult(bool isValid, string? errorMessage)
    {
        IsValid = isValid;
        ErrorMessage = errorMessage;
    }
}
