using System.Runtime.InteropServices;

namespace AltaSoft.DomainPrimitives.XmlDataTypes;

/// <summary>
/// A domain primitive type representing a non-negative integer.
/// </summary>
/// <remarks>
/// The NonNegativeInteger ensures that its value is a non-negative integer (greater than or equal to zero).
/// </remarks>
[StructLayout(LayoutKind.Auto)]
public readonly partial struct NonNegativeInteger : IDomainValue<int>
{
    /// <inheritdoc/>
    public static PrimitiveValidationResult Validate(int value)
    {
        if (value < 0)
            return "value is negative";

        return PrimitiveValidationResult.Ok;
    }
}
