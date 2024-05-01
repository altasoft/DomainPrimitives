namespace AltaSoft.DomainPrimitives.XmlDataTypes;

/// <summary>
/// A domain primitive representing a non-empty string.
/// </summary>
/// <remarks>
/// The NonEmptyString ensures that its value is a non-empty string.
/// </remarks>
[StringLength(1, int.MaxValue)]
public partial class NonEmptyString : IDomainValue<string>
{
    /// <inheritdoc/>
    public static void Validate(string value)
    {
        if (string.IsNullOrEmpty(value))
            throw new InvalidDomainValueException("value is empty string");
    }
}
