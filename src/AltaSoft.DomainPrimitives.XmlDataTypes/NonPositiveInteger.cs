using System.Runtime.InteropServices;

namespace AltaSoft.DomainPrimitives.XmlDataTypes;

/// <summary>
/// A domain primitive type representing a non-positive integer.
/// </summary>
/// <remarks>
/// The NonPositiveInteger ensures that its value is a non-positive integer (less than or equal to zero).
/// </remarks>
[StructLayout(LayoutKind.Auto)]
public readonly partial struct NonPositiveInteger : IDomainValue<int>
{
    /// <inheritdoc/>
    public static PrimitiveValidationResult Validate(int value)
    {
        if (value > 0)
            return "value is positive";

        return PrimitiveValidationResult.Ok;
    }
}
