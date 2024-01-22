namespace AltaSoft.DomainPrimitives.XmlDataTypes;

/// <summary>
/// A domain primitive type representing a non-negative integer.
/// </summary>
/// <remarks>
/// The NonNegativeInteger ensures that its value is a non-negative integer (greater than or equal to zero).
/// </remarks>
public readonly partial struct NonNegativeInteger : IDomainValue<int>
{
    /// <inheritdoc/>
    public static void Validate(int value)
    {
        if (value < 0)
            throw new InvalidDomainValueException("value is negative");
    }

    /// <inheritdoc/>
    public static int Default => 0;
}